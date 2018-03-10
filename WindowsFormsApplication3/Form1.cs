using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GameCore;
using GameDataLayer;
using GameService;
using SeaBattleGame.Properties;
using ViewCell = GameService.ViewCell;

namespace SeaBattleGame
{
    public partial class Form1 : Form
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        
        private RatingForm _ratingForm;
        private readonly GameController _controller;       
        private readonly ViewCell _view = new ViewCell();
        /// <summary>
        /// Occurs when game is started
        /// </summary>
        public event EventHandler BeginGameEvent;
        /// <summary>
        /// Returns or sets players repository
        /// </summary>
        public readonly IRepository<BattlePlayer> Players;
        /// <summary>
        /// Returns or sets player name
        /// </summary>
        public static string PlayerName { get; set; }
        /// <summary>
        /// Initialize a new instance of the <see cref="Form1"/> class
        /// </summary>
        public Form1()
        {
            InitializeComponent();
           
            _controller = new GameController();
            BeginGameEvent += BeginGame;
            _controller.LeftField.DisplayCompletionCell();
            FillTable(tableLayoutPanel1,_controller.LeftField.CellField);
            FillTable(tableLayoutPanel2 ,_controller.RightField.CellField);
            Players = new PlayersRepository();
            var greetingform = new GreetingForm();
            greetingform.Click += (senderSlave, eSlave) =>
            {
                PlayerName = greetingform.textBox1.Text;
            };
            greetingform.FormClosing += (senderSlave, eSlave) =>
            {
                PlayerName = greetingform.textBox1.Text;
            };
            greetingform.ShowDialog();
        }
 
        private void FillTable(TableLayoutPanel table, Cell[,] pictBox)
        {
           
            int sizeCell = table.Width / table.ColumnCount;

            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    pictBox[i, j].Height = _controller.RightField.CellField[i, j].Width = sizeCell;
                    pictBox[i, j].Margin = new Padding(1, 1, 1, 1);          
                    pictBox[i, j].Paint += _view.PaintCell;
                    table.Controls.Add(pictBox[i, j], j, i);
                }
            }
        }
        private void ResetTable()
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    _controller.RightField.CellField[i, j].CellStatus = CellStatus.Empty;
                }
            }
        }
        private void BeforeGame()
        {

            ResetTable();
            _controller.RandomArrangement(_controller.LeftField);
            _controller.LeftField.DisplayCompletionCell();
            FillTable(tableLayoutPanel1, _controller.LeftField.CellField);
            Refresh();
            button3.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
        }
        private void EndGame()
        {

            _controller.RightPlayer.TransferMove -= _controller.Transfer_Move;
            _controller.LeftPlayer.TransferMove -= _controller.Transfer_Move;
            UnsubscriptionOponentField();
            _controller.LeftPlayer.OponentChanged -= Oponent_Changed;
            _controller.GameOver(_controller.LeftStatistics, _controller.RightStatistics);
            AddOrEditPlayer();
            MessageBox.Show(_controller.CountWin != 0
                ? @"Congratulations! You are winner!"
                : @"Sorry, you are loser >_<");

            button3.Visible = true;
            _controller.ResetStatistics(_controller.RightStatistics);
            _controller.ResetStatistics(_controller.LeftStatistics);

            label4.Text = @"Ship amount: " + _controller.RightStatistics.CountShips;
            label5.Text = @"Shot amount: " + _controller.RightStatistics.CountLeftShot;
            label6.Text = @"Ship amount: " + _controller.LeftStatistics.CountShips;
            label7.Text = @"Shot amount: " + _controller.LeftStatistics.CountLeftShot;

        }
        private void BeginGame(object sender, EventArgs e)
        {
            if (radioButtonEasyMode.Checked)
            {
                _controller.GetEasyPlayer();
            }
            else if (radioButtonNormalMode.Checked)
            {
                _controller.GetNormalPlayer();
            }
            button1.Visible = false;
            button2.Visible = false;
            _controller.LeftPlayer.OponentChanged += Oponent_Changed;

            _controller.OnTransferMoveSubscribe();

            _controller.Init();
            SubscriptionOponentField();

        }

        private void ButtonEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.BackColor = Color.Blue;
            button.ForeColor = Color.Gold;
        }

        private void ButtonLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.BackColor = Color.DodgerBlue;
            button.ForeColor = Color.White;
        }

        private void aquaStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var assemblyAqua = Assembly.Load(Resources.StyleAqua);
                var types = assemblyAqua.GetTypes();
                foreach (var t in types)
                {
                    if (!t.ToString().Contains("AquaStyle")) continue;
                    var setstyle = t.GetMethod("SetBackgroundStyle");
                    var obj = Activator.CreateInstance(t);
                    object[] param = { this };
                    if (setstyle != null) setstyle.Invoke(obj, param);

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(@"Sorry. This style temporarily unavailable");
            }
        }

        private void grayStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var assemblyAqua = Assembly.Load(Resources.StyleGray);

                var types = assemblyAqua.GetTypes();
                foreach (var t in types)
                {
                    if (!t.ToString().Contains("GrayStyle")) continue;
                    var setstyle = t.GetMethod("SetBackgroundStyle");
                    var obj = Activator.CreateInstance(t);
                    var param = new object[] { this };
                    if (setstyle != null) setstyle.Invoke(obj, param);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(@"Sorry. This style temporarily unavailable");
            }
        }

        private void topPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ratingForm = new RatingForm();
            _ratingForm.ShowDialog();
        }

        private void aboutGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Sea Battle " + Environment.NewLine + @"Version 1.0.0" + Environment.NewLine + @"Developer: Marina Rybalko");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            panelGameMode.Visible = true;

            BeforeGame();
            _controller.EnabledSwitch(_controller.LeftField.CellField, true);

            foreach (var value in _controller.RightField.CellField)
                value.CellStatus = CellStatus.Empty;


            _controller.RandomArrangement(_controller.RightField);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _controller.RandomArrangement(_controller.LeftField);
            _controller.LeftField.DisplayCompletionCell();

            tableLayoutPanel1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            panelGameMode.Visible = false;

            _controller.EnabledSwitch(_controller.LeftField.CellField, false);
            _controller.EndedGame = false;
            BeginGameEvent?.Invoke(null, EventArgs.Empty);
            var oneMove = (Side)Random.Next(0, 2);
            _controller.Transfer_Move(oneMove == Side.Right ? _controller.LeftPlayer : _controller.RightPlayer,
                EventArgs.Empty);
            _controller.EnabledSwitch(_controller.RightField.CellField, true);
        }

        private void Oponent_Changed(object sender, EventArgs e)
        {
            if (_controller.LeftPlayer.Oponent != null) UnsubscriptionOponentField();

            SubscriptionOponentField();
        }


        private void SubscriptionOponentField()
        {
            foreach (var value in _controller.RightField.CellField)
            {
                value.MouseClick += Cell_Click;
            }
        }
        private void UnsubscriptionOponentField()
        {
            foreach (var value in _controller.RightField.CellField)
            {
                value.MouseClick -= Cell_Click;
            }
        }
        private void Cell_Click(object sender, MouseEventArgs e)
        {
            if (!((HumanPlay)(_controller.LeftPlayer)).CanMove) return;

            var cell = (Cell)sender;
            cell.Enabled = false;

            var shotResult = _controller.LeftPlayer.OponentField.Shot(cell);

            if (shotResult == CellStatus.Miss)
            {
                ((HumanPlay)(_controller.LeftPlayer)).CanMove = false;
                ((HumanPlay)(_controller.LeftPlayer)).CallTransferMove();
            }

            FillTable(tableLayoutPanel1, _controller.LeftField.CellField);
            Refresh();

            label4.Text = @"Ship amount: " + _controller.RightStatistics.CountShips;
            label5.Text = @"Shot amount: " + _controller.RightStatistics.CountLeftShot;
            label6.Text = @"Ship amount: " + _controller.LeftStatistics.CountShips;
            label7.Text = @"Shot amount: " + _controller.LeftStatistics.CountLeftShot;


            if (_controller.RightStatistics.CountShips == 0 || _controller.LeftStatistics.CountShips == 0)
            {
                EndGame();
            }

        }
        private void AddOrEditPlayer()
        {
            var query = Players.GetPlayers(PlayerName);
            if (query != null)
            {
                foreach (var q in query)
                {
                    if (q.WinAmount == null) continue;
                    var win = (int)q.WinAmount + _controller.CountWin;
                    if (q.DefeatAmount == null) continue;
                    var defeat = (int)q.DefeatAmount + _controller.CountDefeat;
                    int rating;
                    if (defeat == 0)
                    {
                        rating = win + 100;

                    }
                    else
                    {
                        rating = (win / defeat) + 100;

                    }
                    q.WinAmount = win;
                    q.DefeatAmount = defeat;
                    q.Rating = rating;
                }

                Players.Save();
            }
            else
            {
                var player = new BattlePlayer
                {
                    Name = PlayerName,
                    WinAmount = _controller.CountWin,
                    DefeatAmount = _controller.CountDefeat
                };
                if (_controller.CountDefeat != 0)
                    player.Rating = _controller.CountWin / _controller.CountDefeat + 100;
                else player.Rating = _controller.CountWin + 100;
                Players.Create(player);
                Players.Save();
            }
        }


    }
}
