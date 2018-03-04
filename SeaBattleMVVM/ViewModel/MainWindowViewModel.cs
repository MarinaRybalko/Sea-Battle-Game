using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GameCore;
using GameDataLayer;
using GameService;
using SeaBattleMVVM.Annotations;
using Brushes = System.Windows.Media.Brushes;
using MessageBox = System.Windows.MessageBox;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SeaBattleMVVM.ViewModel
{
    public class MainWindowViewModel:DependencyObject, INotifyPropertyChanged
    {

        private readonly Model.Model _model;
        private readonly Random _random = new Random();
        private string _shipAmountLeft = "Ship amount: 10 ";
        private string _shotAmountLeft = "Shot amount: 100 ";
        private string _shipAmountRight = "Ship amount: 10 ";
        private string _shotAmountRight = "Shot amount: 100 ";
        private bool _isRightField;
        private bool _isButtonsStartVisible = true;
        private bool _isGameModeNormal = true;
        private bool _isPlaying;
        private bool _isEndButton;
        private bool _isEnabled;



        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        public bool IsEndButton
        {
            get { return _isEndButton; }
            set
            {
                _isEndButton = value;
                OnPropertyChanged("IsEndButton");
            }
        }
        public bool IsButtonsStartVisible
        {
            get { return _isButtonsStartVisible; }
            set
            {
                _isButtonsStartVisible = value;
                OnPropertyChanged("IsButtonsStartVisible");
            }
        }
        public event EventHandler BeginGameEvent;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name = "";
        public BindingArray Status { get; set; } = new BindingArray();
        public BindingArray StatusRightArray { get; set; } = new BindingArray();
        public UniformGrid Grid { get; set; }
        public UniformGrid GridR { get; set; }
        public IEnumerable TopPlayersCollection { get; set; }
        public bool IsGameModeNormal {
            get { return _isGameModeNormal; }
            set
            {
                _isGameModeNormal = value;
                OnPropertyChanged("IsGameModeNormal");
            }
        }
        public string ShipAmountLeft {
            get { return _shipAmountLeft; }
            set
            {
                _shipAmountLeft = value;
                OnPropertyChanged("ShipAmountLeft");
            } }
        public string ShotAmountLeft
        {
            get { return _shotAmountLeft;}
            set {
                _shotAmountLeft = value;
                OnPropertyChanged("ShotAmountLeft");
            }
        } 
        public string ShipAmountRight
        {
            get { return _shipAmountRight; }
            set
            {
                _shipAmountRight = value;
                OnPropertyChanged("ShipAmountRight");
            }
        }
        public string ShotAmountRight {
            get { return _shotAmountRight; }
            set
            {
                _shotAmountRight = value;
                OnPropertyChanged("ShotAmountRight");
            }
        }
    
        public ICommand GenerateFleetCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand InformationCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand TopPlayersCommand { get; set; }
        public ICommand GreetingCommand { get; set; }

        public MainWindowViewModel()
        {

            _model = new Model.Model(new GameController());
            BeginGameEvent += BeginGame;
            GenerateFleetCommand = new DelegateCommand(GenerateFleet);
            StartCommand = new DelegateCommand(Start);
            OkCommand = new DelegateCommand(Ok);
            InformationCommand = new DelegateCommand(Information);
            ExitCommand = new DelegateCommand(Exit);
            TopPlayersCommand = new DelegateCommand(TopPlayersView);
            GreetingCommand = new DelegateCommand(Greeting);
            MouseDownCommand = new DelegateCommand(OnMouseDown);
            UpdateCommand = new DelegateCommand(Update);
            BeforeGame();
            TopPlayersCollection = _model.Players.GetTopTen();
            _model.Game.RandomArrangement(_model.Game.LeftField);
            _model.Game.LeftField.DisplayCompletionCell();
            SetLeftFieldCells();

        }


        private void GenerateFleet(object obj)
        {
            var grid = obj as UniformGrid;

            _model.Game.RandomArrangement(_model.Game.LeftField);
            _model.Game.LeftField.DisplayCompletionCell();
            SetLeftFieldCells();
            if (grid == null) return;
            foreach (var child in grid.Children)
            {
                UpdateCommand.Execute(child);
            }
        }
        private void Start(object obj)
        {
            IsEnabled = true;
            _isPlaying = true;


            var window = obj as Window;
            if (window != null)
            {
                var panel = (Panel)window.Content;
                foreach (var child in panel.Children)
                {
                    if (!(child is Canvas)) continue;
                    foreach (var ccChild in (child as Canvas).Children)
                    {
                        if (Grid != null && ccChild is UniformGrid)
                        {
                            GridR = (UniformGrid)ccChild;
                        }
                        else if (ccChild is UniformGrid) Grid = (UniformGrid)ccChild;

                    }
                }
            }

            foreach (var child in GridR.Children)
            {
                ((Rectangle)child).IsEnabled = true;
            }
            IsButtonsStartVisible = false;
            _model.Game.EnabledSwitch(_model.Game.LeftField.CellField, false);
            _model.Game.EndedGame = false;
            BeginGameEvent?.Invoke(null, EventArgs.Empty);
            var oneMove = (Side)_random.Next(0, 2);
            _model.Game.Transfer_Move(oneMove == Side.Right ? _model.Game.LeftPlayer : _model.Game.RightPlayer,
                EventArgs.Empty);
            _model.Game.EnabledSwitch(_model.Game.RightField.CellField, true);
        }
        private void Ok(object obj)
        {

            ResetRightField();
            GenerateFleet(Grid);
            IsButtonsStartVisible = true;

            BeforeGame();

            _model.Game.EnabledSwitch(_model.Game.LeftField.CellField, true);

            foreach (var value in _model.Game.RightField.CellField)
                value.CellStatus = CellStatus.Empty;
            _model.Game.RandomArrangement(_model.Game.RightField);
        }
        private static void Information(object obj)
        {
            MessageBox.Show(@"Sea Battle " + Environment.NewLine + @"Version 1.0.0" + Environment.NewLine + @"Developer: Marina Rybalko");
        }
        private static void Exit(object obj)
        {
            var window = obj as Window;
            window?.Close();
        }
        private void OnMouseDown(object sender)
        {
            _isPlaying = true;
            var rect = sender as Rectangle;
            if (rect != null && Convert.ToInt32(rect.RadiusX) == 0)
                DrawMissCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == 1)
                DrawCrippledCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == 2)
                DrawMissCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == 3)
                DrawCrippledCell(rect);
            if (rect != null && Convert.ToInt32(rect.RadiusX) == 4)
                DrawDrownedCell(rect);

        }
        private void Update(object sender)
        {

            var rect = sender as Rectangle;
            if (!_isRightField)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == 0)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 1)
                    rect.Fill = new SolidColorBrush(color: Colors.RoyalBlue);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 2)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 3)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 4)
                    DrawDrownedCell(rect);
            }
            else if (!_isPlaying)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == 0)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 1)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 2)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 3)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 4)
                    DrawDrownedCell(rect);
            }
            else if (_isPlaying)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == 0)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 2)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 3)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == 4)
                    DrawDrownedCell(rect);
            }


        }
        private void TopPlayersView(object obj)
        {
          WindowPresenter.Show(1, TopPlayersCollection,ref Name);
        }
        private void Greeting(object obj)
        {
            WindowPresenter.Show(0, null, ref Name);
        }

        private static void DrawDrownedCell([NotNull] Rectangle rectangle)
        {
            if (rectangle == null) throw new ArgumentNullException(nameof(rectangle));
            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.Red,

                    null,

                    new RectangleGeometry(new Rect(0, 0, 33, 33)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(33,33)));
            gGroup.Children.Add(new LineGeometry(new Point(0, 33), new Point(33, 0)));
            var checkers = new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(Brushes.Black, 2), gGroup);

            var checkersDrawingGroup = new DrawingGroup();

            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            var drawingBrush = new DrawingBrush {Drawing = checkersDrawingGroup};

            rectangle.Fill = drawingBrush;
        }
        private static void DrawCrippledCell([NotNull] Rectangle rectangle)
        {
            if (rectangle == null) throw new ArgumentNullException(nameof(rectangle));
            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.RoyalBlue,

                    null,

                    new RectangleGeometry(new Rect(0, 0, 33, 33)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(33, 33)));
            gGroup.Children.Add(new LineGeometry(new Point(0, 33), new Point(33, 0)));
            var checkers = new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(Brushes.Black, 2), gGroup);

            var checkersDrawingGroup = new DrawingGroup();

            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            var drawingBrush = new DrawingBrush {Drawing = checkersDrawingGroup};

            rectangle.Fill = drawingBrush;
        }
        private static void DrawMissCell([NotNull] Rectangle rect)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.Azure,

                    null,

                    new RectangleGeometry(new Rect(0, 0, 33, 33)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new EllipseGeometry(new Point(16, 16), 5, 5));

            var checkers = new GeometryDrawing(new SolidColorBrush(Colors.Black), null, gGroup);

            var checkersDrawingGroup = new DrawingGroup();

            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            var drawingBrush = new DrawingBrush {Drawing = checkersDrawingGroup};

            rect.Fill = drawingBrush;
        }

        private void SetRightFieldCells()
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    StatusRightArray[i, j] = (int)_model.Game.RightField.CellField[i, j].CellStatus;

                }
            }
        }
        private void SetLeftFieldCells()
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    Status[i, j] = (int)_model.Game.LeftField.CellField[i, j].CellStatus;
                }
            }
        }
        private void ResetRightField()
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    StatusRightArray[i, j] = (int)CellStatus.Empty;
                }
            }
            IsEnabled = true;
            _isRightField = true;
            foreach (var child in GridR.Children)
            {
                ((Rectangle)child).IsEnabled = false;
                UpdateCommand.Execute(child);
            }

            _isRightField = false;

        }

        private void Oponent_Changed(object sender, EventArgs e)
        {
            if (_model.Game.LeftPlayer.Oponent != null) UnsubscriptionOponentField();

            SubscriptionOponentField();
        }
        private void SubscriptionOponentField()
        {

            foreach (var value in GridR.Children)
            {
                ((Rectangle)value).MouseDown += Cell_Click;
            }
        }
        private void UnsubscriptionOponentField()
        {
            foreach (var value in GridR.Children)
            {
                ((Rectangle)value).MouseDown -= Cell_Click;
            }
        }

        public void OnPropertyChanged(string propname )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
            
        }

        private void BeforeGame()
        {
            _model.Game.RandomArrangement(_model.Game.RightField);
            _model.Game.RightField.DisplayCompletionCell();
            SetRightFieldCells();
            IsEndButton = false;
            IsButtonsStartVisible = true;

        }
        private void BeginGame(object sender, EventArgs e)
        {
            if (!IsGameModeNormal)
            {
                _model.Game.GetEasyPlayer();
            }
            else if (IsGameModeNormal)
            {
                _model.Game.GetNormalPlayer();
            }
            _model.Game.LeftPlayer.OponentChanged += Oponent_Changed;

            _model.Game.BeginGameMethod();

            _model.Game.Init();
            SubscriptionOponentField();
        }
        private void EndGame()
        {

            _isPlaying = false;
            IsEnabled = false;
            _model.Game.RightPlayer.TransferMove -= _model.Game.Transfer_Move;
            _model.Game.LeftPlayer.TransferMove -= _model.Game.Transfer_Move;
            UnsubscriptionOponentField();
            _model.Game.LeftPlayer.OponentChanged -= Oponent_Changed;
            _model.Game.GameOver(_model.Game.LeftStatistics, _model.Game.RightStatistics);
            AddOrEditPlayer();
            SetLeftFieldCells();
            foreach (var child in Grid.Children)
            {
                UpdateCommand.Execute(child);
            }
            MessageBox.Show(_model.Game.CountWin != 0
                ? @"Congratulations! You are winner!"
                : @"Sorry, you are loser >_<");

            IsEndButton = true;
            _model.Game.ResetStatistics(_model.Game.RightStatistics);
            _model.Game.ResetStatistics(_model.Game.LeftStatistics);

            ShipAmountLeft = @"Ship amount: " + _model.Game.RightStatistics.CountShips;
            ShotAmountLeft = @"Shot amount: " + _model.Game.RightStatistics.CountLeftShot;
            ShipAmountRight = @"Ship amount: " + _model.Game.LeftStatistics.CountShips;
            ShotAmountRight = @"Shot amount: " + _model.Game.LeftStatistics.CountLeftShot;
            ResetRightField();
            foreach (var child in GridR.Children)
            {
                UpdateCommand.Execute(child);
            }

        }

        private void AddOrEditPlayer()
        {
            var query = _model.Players.GetPlayers(Name);
            if (query != null)
            {
                foreach (var q in query)
                {
                    if (q.WinAmount == null) continue;
                    var win = (int)q.WinAmount + _model.Game.CountWin;
                    if (q.DefeatAmount == null) continue;
                    var defeat = (int)q.DefeatAmount + _model.Game.CountDefeat;
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

                _model.Players.Save();
            }
            else
            {
                if(Name=="")return;
                
                var player = new BattlePlayer
                {
                    Name = Name,
                    WinAmount = _model.Game.CountWin,
                    DefeatAmount = _model.Game.CountDefeat
                };
                if (_model.Game.CountDefeat != 0)
                    player.Rating = _model.Game.CountWin / _model.Game.CountDefeat + 100;
                else player.Rating = _model.Game.CountWin + 100;
                _model.Players.Create(player);
                _model.Players.Save();
            }
        }

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {

            SetLeftFieldCells();
            foreach (var cellgrid in Grid.Children)
            {
                UpdateCommand.Execute(cellgrid);
            }

            if (!((HumanPlay)(_model.Game.LeftPlayer)).CanMove) return;

            var cell = (Rectangle)sender;
            cell.IsEnabled = false;
            var index = GridR.Children.IndexOf(cell);
            var shotResult = GetCellOnIndex(index);

            if (shotResult == CellStatus.Miss)
            {
                ((HumanPlay)(_model.Game.LeftPlayer)).CanMove = false;
                ((HumanPlay)(_model.Game.LeftPlayer)).CallTransferMove();
            }


            ShipAmountLeft = @"Ship amount: " + _model.Game.RightStatistics.CountShips;
            ShotAmountLeft = @"Shot amount: " + _model.Game.RightStatistics.CountLeftShot;
            ShipAmountRight = @"Ship amount: " + _model.Game.LeftStatistics.CountShips;
            ShotAmountRight = @"Shot amount: " + _model.Game.LeftStatistics.CountLeftShot;

            _isRightField = true;
            SetRightFieldCells();
            foreach (var child in GridR.Children)
            {

                UpdateCommand.Execute(child);
            }

            _isRightField = false;

            if (_model.Game.RightStatistics.CountShips == 0 || _model.Game.LeftStatistics.CountShips == 0)
            {
                EndGame();
            }


        }

        private CellStatus GetCellOnIndex(int index)
        {
            if (index < 9)
            {

                return _model.Game.LeftPlayer.OponentField.Shot(_model.Game.RightField.CellField[0, index]);
            }
                var x = index / 10;
                var y = index % 10;
                return _model.Game.LeftPlayer.OponentField.Shot(_model.Game.RightField.CellField[x, y]);

        }
       
    }
}
