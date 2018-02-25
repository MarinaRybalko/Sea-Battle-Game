using System;
using System.Diagnostics;
using System.Threading;
using  GameCore;

namespace CustomGamePlugin
{

    public class EasyModeBot : Player
    {
        private CellStatus _shotState;
        protected bool[,] CheckShot = new bool[Field.Size, Field.Size];
        private Location _currentShot;
        protected int IntactCell { get; set; }



        private int _corditateI;
        private int _cordinateJ = 0;
        private Location _firstShot;

        public EasyModeBot(Field field) : base(field)
        {
            _shotState = CellStatus.Miss;
            GeneralFunction.FalseToMatrix(CheckShot);
            IntactCell = Field.Size * Field.Size;
        }

        public override void Move()
        {
            Thread.Sleep(100);


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
            if (_corditateI < Field.Size)
            {
                if (_shotState == CellStatus.Drowned)
                    MarkDrownedShip();

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
        //tested
        protected virtual Location OverrideShot(bool[,] checkShot, int shot)
        {
            return GeneralFunction.FromNumberToLocation(checkShot, shot);
        }


        //tested

    }
}
