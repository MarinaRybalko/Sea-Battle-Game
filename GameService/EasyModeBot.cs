using GameCore;

namespace GameService
{

    public class EasyModeBot : Player
    {

        private CellStatus _shotState;
        protected bool[,] CheckShot = new bool[Field.Size, Field.Size];
        private Location _currentShot;
        protected int IntactCell { get; set; }



        private int _corditateI;
        private int _cordinateJ;
        private Location _firstShot;

        public EasyModeBot(Field field) : base(field)
        {
            _shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(CheckShot);
            IntactCell = Field.Size * Field.Size;
        }

         public override void Move()
        {
            if (_shotState == CellStatus.Drowned)
                MarkDrownedShip();

            Shot();

            if (_shotState == CellStatus.Miss)
                CallTransferMove();

        }

        protected virtual Ship MarkDrownedShip()
        {
            var ship = OponentField.CellField[_currentShot.I, _currentShot.J].ShipIntoCell;

            ship.MarkShip(CheckShot);

            return ship;
        }
        //tested
   
        private void Shot()
        {
            if (_corditateI + _cordinateJ < Field.Size)
            {
                var shot = _cordinateJ;
                _cordinateJ++;
                if (_cordinateJ == 9)
                {
                    _cordinateJ = 0;
                    _corditateI++;
                }

                var newShot = OverrideShot(CheckShot, shot);

                _shotState = OponentField.Shot(OponentField.CellField[newShot.I, newShot.J]);

                if (_shotState == CellStatus.Crippled)
                {
                    _firstShot = newShot;
                }

                _currentShot = newShot;
            }

            if (_shotState != CellStatus.Miss) Move();
        }
        //tested
        protected virtual Location OverrideShot(bool[,] checkShot, int shot)
        {
            return GeneralFunction.FromNumberToLocation(checkShot, shot);
        }

   
        //tested
     
    }
}
