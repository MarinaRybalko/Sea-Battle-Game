using System;
using System.Threading;
using GameCore;

namespace GameService
{

    public class MyBotPlayer:Player
    {
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
            var shot = _random.Next(0, IntactCell);

            var newShot = OverrideShot(CheckShot, shot);

            _shotState = OponentField.Shot( OponentField.CellField[newShot.I, newShot.J]);

            if (_shotState == CellStatus.Crippled)
            {
                _moveState = MoveState.UnknownDirection;
                _oneGuessing = true;
                _firstShot = newShot;
            }
            _currentShot = newShot;

            if (_shotState != CellStatus.Miss) Move();
        }    
        private void GuessingDirection()
        {

            if (_oneGuessing) CountingAllowableDirection();

            int shot = _random.Next(0, _intactDirection);
            _count = 0;

            int numberDirection = -1;

            for (var i = 0; i < Enum.GetValues(typeof( Direction)).Length; i++)
            {
                if (!_chekDirection[i])
                {
                    if (_count == shot)
                    {
                        _chekDirection[i] = true;
                        _intactDirection--;
                        numberDirection = i;
                    }
                    _count++;
                }
            }

            CellStatus shotResult = ShotDirection((Direction)numberDirection, OponentField);

            if (shotResult == CellStatus.Crippled)
            {
                _direction = (Direction)numberDirection;
                _moveState = MoveState.KnownDirection;
            }

            if (_shotState != CellStatus.Miss) Move();
        }
        private void CountingAllowableDirection()
        {
            for (var i = 0; i < _chekDirection.Length; i++) _chekDirection[i] = false;

            _oneGuessing = false;
            _intactDirection = 0;

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
        }
        private void SureShot(Direction direction, Field oponentField)
        {
            var di = 0;
            var dj = 0;

            switch (direction)
            {
                case Direction.Left:
                    dj = -1;
                    break;
                case Direction.Reight:
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

            CellStatus shotState;

            if ((GeneralFunction.PreventionIndexRange(_currentShot.I + di, _currentShot.J + dj))
                && (!CheckShot[_currentShot.I + di, _currentShot.J + dj]))
            {
                shotState = ShotDirection(direction, oponentField);
            }
            else
            {
                ChangeDirection(direction);
                shotState = ShotDirection(direction, oponentField);
            }
            
            if (shotState == CellStatus.Miss) ChangeDirection(direction);
            else Move();
        }
        private void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    _direction = Direction.Reight;
                    break;
                case Direction.Reight:
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
        }
        private CellStatus ShotDirection(Direction direciton, Field oponentField)
        {
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
                case Direction.Reight:
                    dj = 1; di = 0;
                    break;
                case Direction.Bottom:
                    dj = 0; di = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direciton), direciton, null);
            }

            _shotState = oponentField.Shot(oponentField.CellField[_currentShot.I + di, _currentShot.J + dj]);
            CheckShot[_currentShot.I + di, _currentShot.J + dj] = true;


            if (_shotState != CellStatus.Miss)
            {
                _currentShot.I += di;
                _currentShot.J += dj;
            }

            if (_shotState == CellStatus.Drowned)
                _moveState = MoveState.UnknownShipLocation;

            return _shotState;
        }
        protected virtual Location OverrideShot(bool[,] checkShot, int shot)
        {
            return GeneralFunction.FromNumberToLocation(checkShot, shot);
        }
        protected virtual Ship MarkDrownedShip()
        {
            var ship = OponentField.CellField[_currentShot.I, _currentShot.J].ShipIntoCell;

            ship.MarkShip(CheckShot);

            return ship;
        }
        protected virtual void CountingIntactCell()
        {
            IntactCell = 0;

            foreach (var value in CheckShot) if (!value) IntactCell++;
        }
        public MyBotPlayer(Field field) : base(field)
        {
            _shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(CheckShot);
            IntactCell = Field.Size * Field.Size;
        }
        public override void Move()
        {
            Thread.Sleep(100);
            if (_shotState == CellStatus.Drowned)
                MarkDrownedShip();

            CountingIntactCell();

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

            if (_shotState == CellStatus.Miss)
                CallTransferMove();

        }
    }
}
