using System;
using System.Reflection;
using GameCore;

namespace GameService
{

     public class GameController
    {
        public int CountWin { get; private set; } 
        public int CountDefeat { get; private set; } 
        public Player RightPlayer;
        public Player LeftPlayer;
        public Field LeftField { get; set; }
        public Field RightField { get; set; }
        public Statistics RightStatistics { get; set; }
        public Statistics LeftStatistics { get; set; }
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

        public void GetNormalPlayer()
        {
            RightPlayer = new MyBotPlayer(RightField);
            LeftPlayer.Oponent = RightPlayer;
            RightPlayer.Oponent = LeftPlayer;
        }
        public void GetEasyPlayer()
        {
            var fullpath = AppDomain.CurrentDomain.BaseDirectory;
            var dllPath = fullpath.Replace(@"WindowsFormsApplication3\bin\Debug\", @"Plugins\CustomGamePlugin.dll");
            var pluginAssembly = Assembly.LoadFrom(dllPath);

            foreach (var plugin in pluginAssembly.GetTypes())
            {
                if (plugin.BaseType != null && (plugin.BaseType.ToString() != "GameCore.Player"  )) continue;
                RightPlayer = (Player)Activator.CreateInstance(plugin, new object[] { RightField });            
            }
            LeftPlayer.Oponent = RightPlayer;
            RightPlayer.Oponent = LeftPlayer;
        }
        public bool EndedGame { get; set; } 
        public void BeginGameMethod()
        {         
            RightPlayer.TransferMove += Transfer_Move;
            LeftPlayer.TransferMove += Transfer_Move;
        }
        public void Init()
        {
            RightPlayer.OwnField.MadeShot += MadeShot;
            LeftPlayer.OwnField. MadeShot += MadeShot;
            RightStatistics.CountLeftShot= Field.Size * Field.Size;
            RightStatistics.CountShips = Field.ShipsCount;
            LeftStatistics.CountShips = Field.ShipsCount;
            LeftStatistics.CountLeftShot = Field.Size * Field.Size;
        }
        public void ResetStatistics(Statistics statistics)
        {
            statistics.CountShips = Field.ShipsCount;
            statistics.CountLeftShot = Field.Size * Field.Size;
        }
        public void Transfer_Move(object sender, EventArgs e)
        {
            var player = (Player)sender;
           
            player.Oponent.Move();
        }
        public void MadeShot(object sender, EventArgs e)
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
        public void GameOver(Statistics lStatistics, Statistics rStatistics)
        {
            RightPlayer.OwnField.MadeShot -= MadeShot;
            LeftPlayer.OwnField.MadeShot -= MadeShot;


            if (lStatistics.CountShips > rStatistics.CountShips && (lStatistics.CountShips==0|| rStatistics.CountShips==0))
            {
                CountWin++;


            }
            else
            {
                CountDefeat++;
            }

        }
        public void RandomArrangement(Field field)
        {
            foreach (var value in field.RandomShips.ListShips)
                value.Destruction();

           ((RandomFieldAlgorithm) field.RandomShips).ListShips.Clear();

            ((RandomFieldAlgorithm)field.RandomShips).NewArrangement(field);
        }
    }
    }

