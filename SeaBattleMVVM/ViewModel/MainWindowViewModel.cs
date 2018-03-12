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
using LocalizatorHelper;
using SeaBattleMVVM.Annotations;
using Brushes = System.Windows.Media.Brushes;
using MessageBox = System.Windows.MessageBox;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SeaBattleMVVM.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        
        private const int TopPlayersIndex = 1;
        private const int GreetingIndex = 0;
        private const int RatingConst = 100;
        private readonly Model.Model _model;
        private readonly Random _random = new Random();
        private string _shipAmountLeft;
        private string _shotAmountLeft;
        private string _shipAmountRight;
        private string _shotAmountRight;
        private bool _isRightField;
        private bool _isButtonsStartVisible = true;
        private bool _isGameModeNormal = true;
        private bool _isPlaying;
        private bool _isEndButton;
        private bool _isEnabled;

        /// <summary>
        /// Returns or sets a value indicating whether this element is enabled in the user interface
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Returns or sets visibility for ok button
        /// </summary>
        public bool IsEndButton
        {
            get { return _isEndButton; }
            set
            {
                _isEndButton = value;
                OnPropertyChanged("IsEndButton");
            }
        }

        /// <summary>
        /// Returns or sets visibility for start and generate fleet buttons 
        /// </summary>
        public bool IsButtonsStartVisible
        {
            get { return _isButtonsStartVisible; }
            set
            {
                _isButtonsStartVisible = value;
                OnPropertyChanged("IsButtonsStartVisible");
            }
        }

        /// <summary>
        /// Occurs when game is started
        /// </summary>
        public event EventHandler BeginGameEvent;

        /// <summary>
        /// Occurs when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns or sets player name
        /// </summary>
        public string Name = "";

        /// <summary>
        /// Returns or sets array of left field cells 
        /// </summary>
        public BindingArray Status { get; set; } = new BindingArray();

        /// <summary>
        /// Returns or sets right of left field cells 
        /// </summary>
        public BindingArray StatusRightArray { get; set; } = new BindingArray();

        /// <summary>
        /// Returns or sets left field display 
        /// </summary>
        public UniformGrid Grid { get; set; }

        /// <summary>
        /// Returns or sets right field display 
        /// </summary>
        public UniformGrid GridR { get; set; }

        /// <summary>
        /// Returns or sets top players collection
        /// </summary>
        public IEnumerable TopPlayersCollection { get; set; }

        /// <summary>
        /// Returns or sets a value indicating whether this game mode is normal
        /// </summary>
        public bool IsGameModeNormal
        {
            get { return _isGameModeNormal; }
            set
            {
                _isGameModeNormal = value;
                OnPropertyChanged("IsGameModeNormal");
            }
        }

        /// <summary>
        ///  Returns or sets a value indicating ship amount of left field
        /// </summary>
        public string ShipAmountLeft
        {
            get { return _shipAmountLeft; }
            set
            {
                _shipAmountLeft = value;
                OnPropertyChanged("ShipAmountLeft");
            }
        }

        /// <summary>
        ///  Returns or sets a value indicating shot amount for left player
        /// </summary>
        public string ShotAmountLeft
        {
            get { return _shotAmountLeft; }
            set
            {
                _shotAmountLeft = value;
                OnPropertyChanged("ShotAmountLeft");
            }
        }

        /// <summary>
        ///  Returns or sets a value indicating ship amount of right field
        /// </summary>
        public string ShipAmountRight
        {
            get { return _shipAmountRight; }
            set
            {
                _shipAmountRight = value;
                OnPropertyChanged("ShipAmountRight");
            }
        }

        /// <summary>
        ///  Returns or sets a value indicating shot amount for right player
        /// </summary>
        public string ShotAmountRight
        {
            get { return _shotAmountRight; }
            set
            {
                _shotAmountRight = value;
                OnPropertyChanged("ShotAmountRight");
            }
        }

        /// <summary>
        /// Returns or sets a command that generate fleet for left field
        /// </summary>
        public ICommand GenerateFleetCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that indicating game start
        /// </summary>
        public ICommand StartCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that indicating game end
        /// </summary>
        public ICommand OkCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that provides information about game
        /// </summary>
        public ICommand InformationCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that end game and close application
        /// </summary>
        public ICommand ExitCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that processes MouseDown event
        /// </summary>
        public ICommand MouseDownCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that updates grid cells
        /// </summary>
        public ICommand UpdateCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that  gets top ten best players
        /// </summary>
        public ICommand TopPlayersCommand { get; set; }

        /// <summary>
        /// Returns or sets a command that opens greeting window
        /// </summary>
        public ICommand GreetingCommand { get; set; }

        /// <summary>
        /// Returns or sets command for change language to russian
        /// </summary>
        public ICommand RussianCommand { get; set; }

        /// <summary>
        /// Returns or sets command for change language to english
        /// </summary>
        public ICommand EnglishCommand { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="MainWindowViewModel"/> class
        /// </summary>
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
            RussianCommand = new DelegateCommand(Russian);
            EnglishCommand = new DelegateCommand(English);
            ShipAmountLeft = Field.ShipsCount+" ";
            ShipAmountRight = Field.ShipsCount+" ";
            ShotAmountLeft = Field.Size * Field.Size+" ";
            ShotAmountRight = Field.Size * Field.Size + " ";
            ResourceManagerService.RegisterManager("MainWindowRes", MainWindowRes.ResourceManager, true);
            
            BeforeGame();
            TopPlayersCollection = _model.Players.GetTopTen();
            _model.Game.RandomArrangement(_model.Game.LeftField);
            _model.Game.LeftField.DisplayCompletionCell();
            SetLeftFieldCells();
           

        }

        private void Russian(object obj)
        {
           
            ResourceManagerService.ChangeLocale("ru-RU");

        }

        private void English(object obj)
        {
     
            ResourceManagerService.ChangeLocale("en-US");
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

            _model.Game.RandomArrangement(_model.Game.RightField);
            _model.Game.RightField.DisplayCompletionCell();
            SetRightFieldCells();
            var window = obj as Window;
            if (window != null)
            {
                var panel = (Panel) window.Content;
                foreach (var child in panel.Children)
                {
                    if (!(child is Canvas)) continue;
                    foreach (var ccChild in (child as Canvas).Children)
                    {
                        if (Grid != null && ccChild is UniformGrid)
                        {
                            GridR = (UniformGrid) ccChild;
                        }
                        else if (ccChild is UniformGrid) Grid = (UniformGrid) ccChild;

                    }
                }
            }

            foreach (var child in GridR.Children)
            {
                ((Rectangle) child).IsEnabled = true;
            }

            IsButtonsStartVisible = false;
            _model.Game.EnabledSwitch(_model.Game.LeftField.CellField, false);
            _model.Game.EndedGame = false;
            BeginGameEvent?.Invoke(null, EventArgs.Empty);
            var oneMove = (Side) _random.Next(0, Enum.GetNames(typeof(Side)).Length);
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

        private void Information(object obj)
        {
            var str = ResourceManagerService.GetResourceString("MainWindowRes", "Inform_message");
            MessageBox.Show(str.Replace(@"{\n}", Environment.NewLine));

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
            if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Empty)
                DrawMissCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Completion)
                DrawCrippledCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Miss)
                DrawMissCell(rect);
            else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Crippled)
                DrawCrippledCell(rect);
            if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Drowned)
                DrawDrownedCell(rect);

        }

        private void Update(object sender)
        {

            var rect = sender as Rectangle;
            if (!_isRightField)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Empty)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Completion)
                    rect.Fill = new SolidColorBrush(color: Colors.RoyalBlue);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Miss)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Crippled)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Drowned)
                    DrawDrownedCell(rect);
            }
            else if (!_isPlaying)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Empty)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Completion)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Miss)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Crippled)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Drowned)
                    DrawDrownedCell(rect);
            }
            else if (_isPlaying)
            {
                if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Empty)
                    rect.Fill = new SolidColorBrush(color: Colors.Azure);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Miss)
                    DrawMissCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Crippled)
                    DrawCrippledCell(rect);
                else if (rect != null && Convert.ToInt32(rect.RadiusX) == (int)CellStatus.Drowned)
                    DrawDrownedCell(rect);
            }


        }

        private void TopPlayersView(object obj)
        {
            WindowPresenter.Show(TopPlayersIndex, TopPlayersCollection, ref Name);
        }

        private void Greeting(object obj)
        {
            WindowPresenter.Show(GreetingIndex, null, ref Name);
        }

        private void DrawDrownedCell([NotNull] Rectangle rectangle)
        {
            var rectWidth = Grid.Width / Field.Size;
            var rectHeight = Grid.Height / Field.Size;
            if (rectangle == null) throw new ArgumentNullException(nameof(rectangle));

            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.Red,

                    null,

                    new RectangleGeometry(new Rect(0, 0,rectWidth, rectHeight)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(rectWidth, rectHeight)));
            gGroup.Children.Add(new LineGeometry(new Point(0, rectHeight), new Point(rectWidth, 0)));
            var checkers = new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(Brushes.Black, 2), gGroup);

            var checkersDrawingGroup = new DrawingGroup();

            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            var drawingBrush = new DrawingBrush {Drawing = checkersDrawingGroup};

            rectangle.Fill = drawingBrush;
        }

        private  void DrawCrippledCell([NotNull] Rectangle rectangle)
        {
            if (rectangle == null) throw new ArgumentNullException(nameof(rectangle));
            var rectWidth = Grid.Width / Field.Size;
            var rectHeight = Grid.Height / Field.Size;
            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.RoyalBlue,

                    null,

                    new RectangleGeometry(new Rect(0, 0, rectWidth, rectHeight)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(rectWidth, rectHeight)));
            gGroup.Children.Add(new LineGeometry(new Point(0, rectHeight), new Point(rectWidth, 0)));
            var checkers = new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(Brushes.Black, 2), gGroup);

            var checkersDrawingGroup = new DrawingGroup();

            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            var drawingBrush = new DrawingBrush {Drawing = checkersDrawingGroup};

            rectangle.Fill = drawingBrush;
        }

        private void DrawMissCell([NotNull] Rectangle rect)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            var rectWidth = Grid.Width / Field.Size;
            var rectHeight = Grid.Height / Field.Size;
            var missPointSize = 5;
            var backgroundSquare =

                new GeometryDrawing(

                    Brushes.Azure,

                    null,

                    new RectangleGeometry(new Rect(0, 0, rectWidth, rectHeight)));
            var gGroup = new GeometryGroup();

            gGroup.Children.Add(new EllipseGeometry(new Point(rectWidth/2, rectHeight/2), missPointSize, missPointSize));

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
                    StatusRightArray[i, j] = (int) _model.Game.RightField.CellField[i, j].CellStatus;

                }
            }
        }

        private void SetLeftFieldCells()
        {
            for (var i = 0; i < Field.Size; i++)
            {
              
                for (var j = 0; j < Field.Size; j++)
                {     
                    Status[i, j] = ((int)_model.Game.LeftField.CellField[i, j].CellStatus);                   
                }
            }
        }

        private void ResetRightField()
        {
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    StatusRightArray[i, j] = (int) CellStatus.Empty;
                }
            }

            IsEnabled = true;
            _isRightField = true;
            foreach (var child in GridR.Children)
            {
                ((Rectangle) child).IsEnabled = false;
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
                ((Rectangle) value).MouseDown += Cell_Click;
            }
        }

        private void UnsubscriptionOponentField()
        {
            foreach (var value in GridR.Children)
            {
                ((Rectangle) value).MouseDown -= Cell_Click;
            }
        }

        private void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));

        }

        private void BeforeGame()
        {
           
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

            _model.Game.OnTransferMoveSubscribe();

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
                ? ResourceManagerService.GetResourceString("MainWindowRes", "Winner_message")
                : ResourceManagerService.GetResourceString("MainWindowRes", "Loser_message"));



            IsEndButton = true;
            _model.Game.ResetStatistics(_model.Game.RightStatistics);
            _model.Game.ResetStatistics(_model.Game.LeftStatistics);

            ShipAmountLeft = _model.Game.RightStatistics.CountShips+" ";
            ShotAmountLeft = _model.Game.RightStatistics.CountLeftShot+" ";
            ShipAmountRight = _model.Game.LeftStatistics.CountShips+" ";
            ShotAmountRight = _model.Game.LeftStatistics.CountLeftShot+" ";

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
                    var win = (int) q.WinAmount + _model.Game.CountWin;
                    if (q.DefeatAmount == null) continue;
                    var defeat = (int) q.DefeatAmount + _model.Game.CountDefeat;
                    int rating;
                    if (defeat == 0)
                    {
                        rating = win + RatingConst;

                    }
                    else
                    {
                        rating = (win / defeat) + RatingConst;

                    }

                    q.WinAmount = win;
                    q.DefeatAmount = defeat;
                    q.Rating = rating;
                }

                _model.Players.Save();
            }
            else
            {
                if (Name == "") return;

                var player = new BattlePlayer
                {
                    Name = Name,
                    WinAmount = _model.Game.CountWin,
                    DefeatAmount = _model.Game.CountDefeat
                };
                if (_model.Game.CountDefeat != 0)
                    player.Rating = _model.Game.CountWin / _model.Game.CountDefeat + RatingConst;
                else player.Rating = _model.Game.CountWin + RatingConst;
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

            if (!((HumanPlay) (_model.Game.LeftPlayer)).CanMove) return;

            var cell = (Rectangle) sender;
            
            cell.IsEnabled = false;
            var index = GridR.Children.IndexOf(cell);
            var shotResult = GetCellOnIndex(index);

            if (shotResult == CellStatus.Miss)
            {
                ((HumanPlay) (_model.Game.LeftPlayer)).CanMove = false;
                ((HumanPlay) (_model.Game.LeftPlayer)).CallTransferMove();
            }


            ShipAmountLeft = _model.Game.RightStatistics.CountShips+" ";
            ShotAmountLeft = _model.Game.RightStatistics.CountLeftShot+" ";
            ShipAmountRight = _model.Game.LeftStatistics.CountShips+" ";
            ShotAmountRight = _model.Game.LeftStatistics.CountLeftShot+" ";

           
            SetRightFieldCells();
            _isRightField = true;
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
            if (index < Field.Size-1)
            {

                return _model.Game.LeftPlayer.OponentField.Shot(_model.Game.RightField.CellField[0, index]);
            }

            var x = index / 10;
            var y = index % 10;
            return _model.Game.LeftPlayer.OponentField.Shot(_model.Game.RightField.CellField[x, y]);

        }    
    }
}
