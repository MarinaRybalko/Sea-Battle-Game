using System;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Media;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeaColor = System.Drawing.Color;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace SeaBattleGame
{

    static public class Statistics
    {
        public static int countWin { get; set; }
        public static int countDefeat { get; set; }
        class GameStatistics
        {
            public Label labelShip;
            public Label labelShot;
            public Label namePlayer;

            int countLeftShot;
            int countShips;

            public GameStatistics(Label namePlayer, Label labelShip, Label labelShot)
            {
                this.labelShip = labelShip;
                this.labelShot = labelShot;
                this.namePlayer = namePlayer;
                countWin = 0;
                countShips = Field.ShipsCount;
                countLeftShot = Field.Size * Field.Size;
            }

            public int CountShips
            {
                set
                {
                    countShips = value;
                    labelShip.Text = shipsWord + value;
                    if (countShips == 0)
                    {
                        GameOver(this);

                    }
                }
                get { return countShips; }
            }

            public int CountLeftShot
            {
                get { return countLeftShot; }
                set
                {
                    countLeftShot = value;
                    labelShot.Text = shotWord + countLeftShot;
                }
            }
        }

        static Player rightPlayer;
        static Player leftPlayer;

        static GameStatistics rightStatistics;
        static GameStatistics leftStatistics;

        static Random random = new Random(DateTime.Now.Millisecond);

        enum Side { Left, Right }

        public static bool EndedGame { get; set; }

        public static event EventHandler BeginGame;
        public static event EventHandler EndGame;
        public static event EventHandler BeforeGame;

        static string shipsWord = "Ship amount:";
        static string shotWord = "Shot amount:";


        static SeaColor moveColor = SeaColor.Gold;
        static SeaColor passColor = SeaColor.Black;


        public static void GetLabel(Label rightShips, Label rightShot, Label rightName,
            Label leftShips, Label leftShot, Label leftName)
        {
            leftStatistics = new GameStatistics(leftName, leftShips, leftShot);
            rightStatistics = new GameStatistics(rightName, rightShips, rightShot);



            Init();
        }

        static void Init()
        {


            Form1.RightField.MadeShot += Made_Shot;
            Form1.LeftField.MadeShot += Made_Shot;
        }

        public static void Begin_Clicked(object sender, EventArgs e)
        {

            EnabledSwitch(Form1.LeftField.CellField, false);

            if (MouseEvent.BeginArround) MouseEvent.EndedArround();

            EndedGame = false;
            BeginGame(null, EventArgs.Empty);

            leftPlayer = Form1.LeftPlayer;
            rightPlayer = Form1.RightPlayer;

            Side OneMove = (Side)random.Next(0, 2);

            if (OneMove == Side.Right)
            {
                Transfer_Move(leftPlayer, EventArgs.Empty);
            }
            else
            {
                Transfer_Move(rightPlayer, EventArgs.Empty);
            }

            EnabledSwitch(Form1.RightField.CellField, true);
           
        }

        public static void Ok_Clicked(object sender, EventArgs e)
        {

            BeforeGame(null, EventArgs.Empty);
            EnabledSwitch(Form1.LeftField.CellField, true);

            foreach (var value in Form1.RightField.CellField)
                value.RenderingMode = CellStatus.Empty;

            Form1.LeftField.RandomShips.RandomArrangement();
            Form1.LeftField.DisplayCompletionCell();

            Form1.RightField.RandomShips.RandomArrangement();


            resetStatistika(rightStatistics);
            resetStatistika(leftStatistics);

        }

        static void resetStatistika(GameStatistics statistika)
        {
            statistika.CountShips = Field.ShipsCount;
            statistika.CountLeftShot = Field.Size * Field.Size;
            statistika.namePlayer.ForeColor = passColor;
        }

        public static void Transfer_Move(object sender, EventArgs e)
        {
            Player player = (Player)sender;

            if (player == rightPlayer)
            {
                rightStatistics.namePlayer.ForeColor = passColor;
                leftStatistics.namePlayer.ForeColor = moveColor;
            }
            else
            {
                leftStatistics.namePlayer.ForeColor = passColor;
                rightStatistics.namePlayer.ForeColor = moveColor;
            }

            Refresh();

            player.Oponent.Move();
        }

        static void Refresh()
        {
            rightStatistics.namePlayer.Invalidate();
            leftStatistics.namePlayer.Invalidate();
            leftStatistics.namePlayer.Update();
            rightStatistics.namePlayer.Update();
            Thread.Sleep(300);
        }

        static void Made_Shot(object sender, EventArgs e)
        {
            Field field = (Field)sender;

            if (field == Form1.RightField)
            {
                leftStatistics.CountLeftShot--;
            }
            else
            {
                rightStatistics.CountLeftShot--;
            }

            CellStatus result = ((shotEventArgs)e).shotResult;

            if (result == CellStatus.Drowned)
            {
                if (field == Form1.RightField)
                {
                    rightStatistics.CountShips--;
                }
                else
                {
                    leftStatistics.CountShips--;
                }
            }


        }

        static void GameOver(GameStatistics lossStatistics)
        {
            EndedGame = true;
            EnabledSwitch(Form1.RightField.CellField, false);

            GameStatistics winStatistics;

            if (lossStatistics.Equals(rightStatistics))
            {

                winStatistics = leftStatistics;
                countWin++;
                MessageBox.Show("You are winner!! Congratulations!!");


            }
            else
            {
                winStatistics = rightStatistics;
                countDefeat++;
                MessageBox.Show("You are loser!! Sorry!!");

            }
            SaveResult();

            EndGame(null, EventArgs.Empty);


        }

        static void EnabledSwitch(SeaBattlePicture[,] Matrix, bool value)
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    Matrix[i, j].Enabled = value;
                }
            }
        }
        private static void SaveResult()
        {

            using (PlayersDBEntities db = new PlayersDBEntities())
            {

                if (!IsExist())
                {
                    BattlePlayer player = new BattlePlayer()
                    {

                        Name = Form1.playerName,
                        WinAmount = countWin,
                        DefeatAmount = countDefeat,

                    };
                    if (countDefeat == 0)
                    {
                        player.Rating = (double)countWin;
                    }
                    else
                    {
                        player.Rating = (double)(countWin / countDefeat);
                    }

                    db.BattlePlayer.Add(player);
                    db.SaveChanges();
                }
            }
        }
        private static bool IsExist()
        {
            using (PlayersDBEntities db = new PlayersDBEntities())
            {

                var query = from p in db.BattlePlayer
                            where p.Name == Form1.playerName
                            select p;
                int Win=0;
                int Defeat = 0;
                int rating = 0;
                if (query.Count() != 0)
                {
                    foreach (var q in query)
                    {

                        Win = (int)q.WinAmount + countWin;
                        Defeat = (int)q.DefeatAmount + countDefeat;
                        if (Defeat == 0)
                        {
                            rating = Win;

                        }
                        else
                        {
                            rating = Win / Defeat;

                        }
                        q.WinAmount = Win;
                        q.DefeatAmount = Defeat;
                        q.Rating = rating;
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
               
           }
          }
        }
    }

