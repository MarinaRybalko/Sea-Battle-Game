using System;
using System.Reflection;
using GameCore;
using NLog;

namespace GameService
{

     public class GameController
     {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
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
        /// <summary>
        /// Initialize normal mode player and sets oponents to players
        /// </summary>
        public void GetNormalPlayer()
        {
            Logger.Debug("Attempt to create Right Player instance and set oponents for players");
            RightPlayer = new MyBotPlayer(RightField);         
            LeftPlayer.Oponent = RightPlayer;          
            RightPlayer.Oponent = LeftPlayer;
            Logger.Debug("Attempt to create Right Player instance and set oponents for players successfully completed" + RightPlayer.GetHashCode());
        }
         /// <summary>
         /// Initialize easy mode player and sets oponents to players
         /// </summary>
        public void GetEasyPlayer()
        {
            Logger.Debug("Attempt to get full path and load library CustomGamePlagin.dll");
            var fullpath = AppDomain.CurrentDomain.BaseDirectory;
          
            var dllPath = string.Empty;
            if (fullpath.Contains(@"WindowsFormsApplication3\bin\Debug\"))
            {
                dllPath = fullpath.Replace(@"WindowsFormsApplication3\bin\Debug\", @"Plugins\CustomGamePlugin.dll");
            }
            else if (fullpath.Contains(@"SeaBattleMVVM\bin\Debug\"))
            {
                dllPath = fullpath.Replace(@"SeaBattleMVVM\bin\Debug\", @"Plugins\CustomGamePlugin.dll");
            }
            var pluginAssembly = Assembly.LoadFrom(dllPath);
            Logger.Debug("Attempt to get full path and load library CustomGamePlagin.dll successfully completed.Full path: "+fullpath);
            Logger.Debug("Attempt to create Right Player instance and set oponents for players");
            foreach (var plugin in pluginAssembly.GetTypes())
            {
                if (plugin.BaseType != null && (plugin.BaseType.ToString() != "GameCore.Player"  )) continue;
               
                RightPlayer = (Player)Activator.CreateInstance(plugin, new object[] { RightField });            
            }
           
            LeftPlayer.Oponent = RightPlayer;
            RightPlayer.Oponent = LeftPlayer;
            Logger.Debug("Attempt to create Right Player instance and set oponents for players successfully completed" + RightPlayer.GetHashCode());
        }
         /// <summary>
         /// Returns or sets end game flag
         /// </summary>
        public bool EndedGame { get; set; } 
         /// <summary>
         /// Subscribes players on transfer move event 
         /// </summary>
        public void OnTransferMoveSubscribe()
        {
            Logger.Debug("Attempt to subscribe to an event TransferMove for players");
            RightPlayer.TransferMove += Transfer_Move;        
            LeftPlayer.TransferMove += Transfer_Move;
            Logger.Debug("Attempt to subscribe to an event TransferMove for players successfully completed");
        }
        /// <summary>
        /// Subscribes players on made shot event and resets game statistics
        /// </summary>
        public void Init()
        {
            Logger.Debug("Attempt to subscribe to an event MadeShot for players");
            RightPlayer.OwnField.MadeShot += MadeShot;        
            LeftPlayer.OwnField. MadeShot += MadeShot;
            Logger.Debug("Attempt to subscribe to an event MadeShot for players successfully completed");
            Logger.Debug("Attempt to initialize right and  left statistics");
            RightStatistics.CountLeftShot= Field.Size * Field.Size;
            RightStatistics.CountShips = Field.ShipsCount;           
            LeftStatistics.CountShips = Field.ShipsCount;
            LeftStatistics.CountLeftShot = Field.Size * Field.Size;
            Logger.Debug("Attempt to initialize right and  left statistics successfully completed. Right statistic: "+RightStatistics.CountLeftShot+" "+RightStatistics.CountShips + "Left statistic: "+LeftStatistics.CountLeftShot+" "+LeftStatistics.CountShips);
        }/// <summary>
        /// Reset definite statistics
        /// </summary>
        /// <param name="statistics"></param>
        public void ResetStatistics(Statistics statistics)
        {
            Logger.Debug("Attempt to reset certain Statistics");
            statistics.CountShips = Field.ShipsCount;
            statistics.CountLeftShot = Field.Size * Field.Size;
            Logger.Debug("Attempt to reset certain Statistics successfully completed");
        }
         /// <summary>
         /// Transfer move to another player
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        public void Transfer_Move(object sender, EventArgs e)
        {
            Logger.Debug("Attempt to get current player and transfer move to another player");
            var player = (Player)sender;
           
            player.Oponent.Move();
            Logger.Debug("Attempt to get current player and transfer move to another player successfully completed" + player);
        }
         /// <summary>
         /// Made shot and change statistics
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        public void MadeShot(object sender, EventArgs e)
        {
            Logger.Debug("Attempt to get current player update shot amount ");
            var field = (Field)sender;
         
            if (field == RightField)
            {
               
                RightStatistics.CountLeftShot--;
               

            }
            else
            {
            
                LeftStatistics.CountLeftShot--;
                
            }
            Logger.Debug("Attempt to update shot amount for current statistic successfully completed");
            Logger.Debug("Attempt to get shot result");
            var result = ((ShotEventArgs)e).ShotResult;
            Logger.Debug("Attempt to get shot result successfully completed" + result);
            Logger.Debug("Attempt to update ship amount for cuttenr statistic");
            if (result != CellStatus.Drowned) return;
            if (field == RightField)
            {
               
                RightStatistics.CountShips--;
               
            }
            else
            {
              
                LeftStatistics.CountShips--;
              
            }
            Logger.Debug("Attempt to update ship amount for cuttenr statistic successfully completed");

        }
         /// <summary>
         /// Sets given value to matrix
         /// </summary>
         /// <param name="matrix"></param>
         /// <param name="value"></param>
        public void EnabledSwitch(Cell[,] matrix, bool value)
        {
            Logger.Debug("Attempt to change matrix value from "+value+" to "+!value);
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    matrix[i, j].Enabled = value;
                }
            }
            Logger.Debug("Attempt to change matrix value from " + value + " to " + !value + "successfully completed. Random matrix value is " +matrix[0,0].Enabled);
        }
        /// <summary>
        /// Unubscribes players from made shot event and sets game result
        /// </summary>
        /// <param name="lStatistics"></param>
        /// <param name="rStatistics"></param>
        public void GameOver(Statistics lStatistics, Statistics rStatistics)
        {
            Logger.Debug("Attempt to unsubscribe to an event MadeShot for players");
            RightPlayer.OwnField.MadeShot -= MadeShot;
          
            LeftPlayer.OwnField.MadeShot -= MadeShot;
            Logger.Debug("Attempt to unsubscribe to an event MadeShot for players successfully completed");

            Logger.Debug("Attempt to get game result");
            if (lStatistics.CountShips > rStatistics.CountShips && (lStatistics.CountShips==0|| rStatistics.CountShips==0))
            {
                CountWin++;


            }
            else
            {
                CountDefeat++;
            }
            Logger.Debug("Game result: win amount is "+CountWin+" defeat amount:" +CountDefeat);

        }
        public void RandomArrangement(Field field)
        {
            Logger.Debug("Attemp to clear field from ships and generate new field");
            foreach (var value in field.RandomShips.ListShips)
                value.Destruction();
          
            ((RandomFieldAlgorithm) field.RandomShips).ListShips.Clear();
          
            ((RandomFieldAlgorithm)field.RandomShips).NewArrangement(field);
            Logger.Debug("Attemp to clear field from ships and generate new field successfully completed. Ship amount is " + field.RandomShips.ListShips.Count);
        }
    }
    }

