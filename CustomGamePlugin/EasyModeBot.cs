
using System.Threading;
using  GameCore;

namespace CustomGamePlugin
{

    public class EasyModeBot : Player
    {
        private Location _currentShot;
        private int _corditateI;
        private int _cordinateJ;
        private CellStatus _shotState;
        private readonly bool[,] _checkShot = new bool[Field.Size, Field.Size];
        /// <inheritdoc />
        /// <summary>
        /// Initialize new instance of the <see cref="T:CustomGamePlugin.EasyModeBot" /> class
        /// </summary>
        /// <param name="field"></param>
        public EasyModeBot(Field field) : base(field)
        {
            _shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(_checkShot);
        }
        /// <summary>
        /// Make a move
        /// </summary>
        public override void Move()
        {
            
            Shot();

            if (_shotState == CellStatus.Miss)
                CallTransferMove();

        }
        protected virtual Ship MarkDrownedShip()
        {
            var ship = OponentField.CellField[_currentShot.I, _currentShot.J].ShipIntoCell;

            ship.MarkShip(_checkShot);

            return ship;
        }
        private void Shot()
        {
            if (_corditateI < Field.Size)
            {
                if (_shotState == CellStatus.Drowned)
                    MarkDrownedShip();
                Thread.Sleep(140);

                if (!(_cordinateJ < Field.Size))
                {
                    _cordinateJ = 0;
                    _corditateI++;
                }
                do
                {
                    _shotState = OponentField.Shot(OponentField.CellField[_corditateI, _cordinateJ]);
                    _cordinateJ++;
                    if (_cordinateJ != Field.Size) continue;
                    _cordinateJ = 0;
                    _corditateI++;

                } while (OponentField.CellField[_corditateI, _cordinateJ].CellStatus == CellStatus.Miss);

                _currentShot = new Location(_corditateI, _cordinateJ);

            }

            if (_shotState != CellStatus.Miss) Move();
        }
        protected virtual Location OverrideShot(bool[,] checkShot, int shot)
        {
            return GeneralFunction.FromNumberToLocation(checkShot, shot);
        }

    }
}
