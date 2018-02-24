﻿using System;
using GameCore;

namespace GameService
{

     public class GameController
    {

        public int CountWin { get; private set; } //0
        public int CountDefeat { get; private set; } //0
        public Player RightPlayer;
        public Player LeftPlayer;
        public Field LeftField { get; set; }
        public Field RightField { get; set; }
        public Statistics RightStatistics { get; set; }
        public Statistics LeftStatistics { get; set; }
        public GameStatus GameStatus { get; set; }
        public GameController()
        {
            LeftField = new Field();
            LeftField.RandomShips= new RandomFieldAlgorithm(LeftField);
           
            RightField = new Field();
            RightField.RandomShips=new RandomFieldAlgorithm(RightField);
            LeftPlayer = new HumanPlay(LeftField);
            
            RightStatistics = new Statistics();
            LeftStatistics = new Statistics();

            ResetStatistics(RightStatistics);
            ResetStatistics(LeftStatistics);

        }
        public bool EndedGame { get; set; } 
        //tested 
        public void BeginGameMethod()
        {
            RightPlayer = new MyBotPlayer(RightField) {Oponent = LeftPlayer};
            LeftPlayer.Oponent = RightPlayer;
 
           
            RightPlayer.TransferMove += Transfer_Move;
            LeftPlayer.TransferMove += Transfer_Move;
        }
        //tested
        public void Init()
        {
            RightPlayer.OwnField.MadeShot += Made_Shot;
            LeftPlayer.OwnField. MadeShot += Made_Shot;
            RightStatistics.CountLeftShot= Field.Size * Field.Size;
            RightStatistics.CountShips = Field.ShipsCount;
            LeftStatistics.CountShips = Field.ShipsCount;
            LeftStatistics.CountLeftShot = Field.Size * Field.Size;
        }
         //tested
        public void ResetStatistics(Statistics statistics)
        {
            statistics.CountShips = Field.ShipsCount;
            statistics.CountLeftShot = Field.Size * Field.Size;
        }
        //tested
        public void Transfer_Move(object sender, EventArgs e)
        {
            var player = (Player)sender;
           
            player.Oponent.Move();
        }
        //tested
        public void Made_Shot(object sender, EventArgs e)
        {
            Field field = (Field)sender;

            if (field == RightField)
            {
                RightStatistics.CountLeftShot--;

            }
            else
            {
                LeftStatistics.CountLeftShot--;
            }

            CellStatus result = ((ShotEventArgs)e).ShotResult;

            if (result == CellStatus.Drowned)
            {
                if (field == RightField)
                {
                    RightStatistics.CountShips--;
                }
                else
                {
                    LeftStatistics.CountShips--;
                }
            }
        }
        //tested
        public void EnabledSwitch(Cell[,] matrix, bool value)
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    matrix[i, j].Enabled = value;
                }
            }
        }
        //tested
        public void GameOver(Statistics lStatistics, Statistics rStatistics)
        {
            RightPlayer.OwnField.MadeShot -= Made_Shot;
            LeftPlayer.OwnField.MadeShot -= Made_Shot;

           
            if (lStatistics.CountShips > rStatistics.CountShips)
            {
                CountWin++;


            }
            else
            {
                CountDefeat++;
            }

        }
        //tested
        public void RandomArrangement(Field field)
        {
            foreach (var value in field.RandomShips.ListShips)
                value.Destruction();

           ((RandomFieldAlgorithm) field.RandomShips).ListShips.Clear();

            ((RandomFieldAlgorithm)field.RandomShips).NewArrangement(field);
        }
    }
    }

