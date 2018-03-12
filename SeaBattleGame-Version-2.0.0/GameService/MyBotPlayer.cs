using System;

using System.Threading;
using GameCore;
using NLog;

namespace GameService
{

    public class MyBotPlayer:Player
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private bool _oneGuessing;
        private MoveState _moveState = MoveState.UnknownShipLocation;
        private Direction _direction;
        private CellStatus _shotState;
        private static int DirectionLenght { get; } = Enum.GetNames(typeof(Direction)).Length;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);     
        private readonly bool[] _chekDirection = new bool[DirectionLenght];
        private int _count;
        private Location _currentShot;
        private Location _firstShot;
        protected int IntactCell { get; set; }
        private int _intactDirection = DirectionLenght;
        protected bool[,] CheckShot = new bool[Field.Size, Field.Size];

        private void LongShot()
        {
            Logger.Debug("Attempt to get random point in order to make a shot");
            var shot = _random.Next(0, IntactCell);

            var newShot = OverrideShot(CheckShot, shot);
            Logger.Debug("Attempt to get random point in order to make a shot successfully completed"+newShot);
            Logger.Debug("Attempt to make a shot and get shot result");
            _shotState = OponentField.Shot( OponentField.CellField[newShot.I, newShot.J]);
            if (_shotState == CellStatus.Crippled)
            {
                _moveState = MoveState.UnknownDirection;
                _oneGuessing = true;
                _firstShot = newShot;
            }
            _currentShot = newShot;
            Logger.Debug("Attempt to make a shot and get shot result successfully completed. Shot result is " + _shotState);
            Logger.Debug("Check if player can make another shot");
            if (_shotState != CellStatus.Miss)
            {
                Logger.Debug("Make another shot");
                Move();
            }
            Logger.Debug("Transfer move to oponent");
        }    
        private void GuessingDirection()
        {
            Logger.Debug("Check available direction");
            if (_oneGuessing) CountingAllowableDirection();
            Logger.Debug("Attempt to get random direction among available in order to make a shot");
            var shot = _random.Next(0, _intactDirection);
            Logger.Debug("Attempt to get random direction among available in order to make a shot successfully completed. Random direction is "+shot);
            _count = 0;

            var numberDirection = -1;
            Logger.Debug("Check all directions and mark checked directions");
            for (var i = 0; i < Enum.GetValues(typeof( Direction)).Length; i++)
            {
                if (_chekDirection[i]) continue;
                if (_count == shot)
                {
                    _chekDirection[i] = true;
                    _intactDirection--;
                    numberDirection = i;
                }
                _count++;
            }
            Logger.Debug("Checking all directions and marking checked directions successfully completed");
            Logger.Debug("Attempt to make a shot and get shot result");
            CellStatus shotResult = ShotDirection((Direction)numberDirection, OponentField);
            Logger.Debug("Attempt to make a shot and get shot result successfully completed. Shot result is " + shotResult);
            Logger.Debug("Check if player can make another shot");
            if (shotResult == CellStatus.Crippled)
            {
                _direction = (Direction)numberDirection;
                _moveState = MoveState.KnownDirection;
            }

            Logger.Debug("Check if player can make another shot");
            if (_shotState != CellStatus.Miss)
            {
                Logger.Debug("Make another shot");
                Move();
            }
            Logger.Debug("Transfer move to oponent");
        }
        private void CountingAllowableDirection()
        {
            Logger.Debug("Mark directions like unchecked");
            for (var i = 0; i < _chekDirection.Length; i++) _chekDirection[i] = false;
          
            _oneGuessing = false;
            _intactDirection = 0;
            Logger.Debug("Marking directions like unchecked successfully completed");
            Logger.Debug("Check shot result and increase the number of checked directions. Intact direction is "+_intactDirection);
            if ((_currentShot.J > 0) && (!CheckShot[_currentShot.I, _currentShot.J - 1]))
            {
                 _intactDirection++;               
            }
            else _chekDirection[0] = true;

            if ((_currentShot.I > 0) && (!CheckShot[_currentShot.I - 1, _currentShot.J]))
            {
                _intactDirection++;             
            }
            else _chekDirection[1] = true;

            if ((_currentShot.J < Field.Size - 1) && (!CheckShot[_currentShot.I, _currentShot.J + 1]))
            {
               _intactDirection++;             
            }
            else _chekDirection[2] = true;

            if ((_currentShot.I < Field.Size - 1) && (!CheckShot[_currentShot.I + 1, _currentShot.J]))
            {
                _intactDirection++;                
            }
            else _chekDirection[3] = true;
            Logger.Debug("Checking shot result and marking checked directions successfully completed. Intact direction is " + _intactDirection);
        }
        private void SureShot(Direction direction, Field oponentField)
        {
         Logger.Debug("Check shot direction to make next shot");
            var di = 0;
            var dj = 0;

            switch (direction)
            {
                case Direction.Left:
                    dj = -1;
                    break;
                case Direction.Right:
                    dj = 1;
                    break;
                case Direction.Top:
                    di = -1;
                    break;
                case Direction.Bottom:
                    di = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            Logger.Debug("Checking shot direction successfully completed. Current direction is "+di+" "+dj);
            CellStatus shotState;
            Logger.Debug("Attempt to get shot result in a given direction");
            if ((GeneralFunction.PreventionIndexRange(_currentShot.I + di, _currentShot.J + dj))
                && (!CheckShot[_currentShot.I + di, _currentShot.J + dj]))
            {
                shotState = ShotDirection(direction, oponentField);
                Logger.Debug("Attempt to get shot result in a given direction successfully completed. Shot was right "+shotState);
            }
            else
            {
               
                Logger.Debug("Attempt to change direction to make another shot and get shot result in a given direction . Direction is " + direction);
                ChangeDirection(direction);
                shotState = ShotDirection(direction, oponentField);           
                Logger.Debug("Attempt to change direction and  get shot result in a given direction successfully completed. Shot result is " + shotState);
            }
            Logger.Debug("Attempt to check shot result. Shot result is " + shotState);
            if (shotState == CellStatus.Miss)
            {
               
                Logger.Debug("Attempt to change direction to make another shot. Direction is " + direction);
                ChangeDirection(direction);
                Logger.Debug("Attempt to change direction successfully completed. Direction is " + direction);
                Logger.Debug("Transfer move to oponent");
           
            }
            else
            {
                Logger.Debug("Make another shot");
                Move();
            }
        }
        private void ChangeDirection(Direction direction)
        {
            Logger.Debug("Attempt to change direction. Current direction is "+ direction);
            switch (direction)
            {
                case Direction.Left:
                    _direction = Direction.Right;
                    break;
                case Direction.Right:
                    _direction = Direction.Left;
                    break;
                case Direction.Top:
                    _direction = Direction.Bottom;
                    break;
                case Direction.Bottom:
                    _direction = Direction.Top;
                    break;


                default:
                    throw new ArgumentOutOfRangeException();
            }

            _currentShot = _firstShot;
            Logger.Debug("Attempt to change direction successfully completed. Current direction is " + direction);
        }
        private CellStatus ShotDirection(Direction direciton, Field oponentField)
        {
            Logger.Debug("Check shot direction to make next shot");
            int di;
            int dj;

            switch (direciton)
            {
                case Direction.Left:
                    dj = -1; di = 0;
                    break;
                case Direction.Top:
                    dj = 0; di = -1;
                    break;
                case Direction.Right:
                    dj = 1; di = 0;
                    break;
                case Direction.Bottom:
                    dj = 0; di = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direciton), direciton, null);
            }
            Logger.Debug("Checking shot direction successfully completed. Current direction is " + di + " " + dj);
            Logger.Debug("Attempt to get shot result in a given direction");
            _shotState = oponentField.Shot(oponentField.CellField[_currentShot.I + di, _currentShot.J + dj]);
            CheckShot[_currentShot.I + di, _currentShot.J + dj] = true;
            Logger.Debug("Attempt to get shot result in a given direction successfully completed. Shot result is " + _shotState);
            Logger.Debug("Check whether direction was right");
            if (_shotState != CellStatus.Miss)
            {
                Logger.Debug("Direction was right");
                _currentShot.I += di;
                _currentShot.J += dj;
            }
            Logger.Debug("Check whether direction was right and cell result is drowned");
            if (_shotState == CellStatus.Drowned)
                _moveState = MoveState.UnknownShipLocation;
            Logger.Debug("Checking whether direction was right and cell result is drowned successfully completed. Shot result is " + _shotState);
            return _shotState;
        }
        protected virtual Location OverrideShot(bool[,] checkShot, int shot)
        {
            Logger.Debug("Attempt to translate random shot into location");
            
            return GeneralFunction.FromNumberToLocation(checkShot, shot);
        }
        protected virtual Ship MarkDrownedShip()
        {
            Logger.Debug("Attempt to mark drowned ship");
            var ship = OponentField.CellField[_currentShot.I, _currentShot.J].ShipIntoCell;

            ship.MarkShip(CheckShot);
            Logger.Debug("Attempt to mark drowned ship successfully completed.");
            return ship;
        }
        protected virtual void CountingIntactCell()
        {
            IntactCell = 0;
            Logger.Debug("Attempt to count intact cells. Intact cells amount is " + IntactCell);
            foreach (var value in CheckShot) if (!value) IntactCell++;
            Logger.Debug("Attempt to count intact cells successfully completed. Intact cells amount is " + IntactCell);
        }

       
        /// <summary>
        /// Initialize a new instance of the <see cref="MyBotPlayer"/>  class
        /// </summary>
        /// <param name="field"></param>
        public MyBotPlayer(Field field) : base(field)
        {
            _shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(CheckShot);
            IntactCell = Field.Size * Field.Size;
        }
        /// <summary>
        /// Make a move
        /// </summary>
        public override void Move()
        {
            Thread.Sleep(100);
            Logger.Debug("Attempt to make a move and  mark drowned ship");
         
            if (_shotState == CellStatus.Drowned)
                MarkDrownedShip();
            Logger.Debug("Attempt to make a move and mark drowned ship successfully completed.");
            Logger.Debug("Attempt to count intact cells");
            CountingIntactCell();
            Logger.Debug("Attempt to count intact cells successfully completed.");
            Logger.Debug("Make shot according to previous shots and get shot result");
            switch (_moveState)
            {
                case MoveState.UnknownShipLocation:
                    LongShot();
                    break;
                case MoveState.UnknownDirection:
                    GuessingDirection();
                    break;
                case MoveState.KnownDirection:
                    SureShot(_direction, OponentField);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Logger.Debug("Making shot according to previous shots and getting shot result successfully completed. Shot result is "+_shotState);

            if (_shotState == CellStatus.Miss)
            {
                Logger.Debug("Transfer move to oponent");
                CallTransferMove();
            }
            Logger.Debug("Make another shot");
            Logger.Debug("Attempt to make a move successfully completed.");
         
        }
    }
}
