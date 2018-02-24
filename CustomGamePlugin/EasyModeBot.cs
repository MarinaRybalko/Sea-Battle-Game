using System;
using System.Diagnostics;
using  GameCore;

namespace CustomGamePlugin
{

    public class EasyModeBot : Player, IMove
    {
        private bool _oneGuessing;


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

        public void Move()
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

          
         

            if (_cordinateJ + _corditateI < Field.Size)
                // var newShot = OverrideShot(CheckShot, shot);
            {
                _shotState = OponentField.Shot(OponentField.CellField[_cordinateJ + _corditateI, _cordinateJ]);

                if (_shotState == CellStatus.Crippled)
                {
                    _firstShot = new Location(_cordinateJ + _corditateI, _cordinateJ);
                }

                _currentShot = new Location(_cordinateJ + _corditateI, _cordinateJ);
                _cordinateJ++;
                if (_cordinateJ == 9)
                {
                    _cordinateJ = 0;
                    _corditateI++;
                }
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
