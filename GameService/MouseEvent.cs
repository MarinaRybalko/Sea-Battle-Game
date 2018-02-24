using System.Windows.Forms;
using GameCore;

namespace GameService
{

    public static class MouseEvent
    {
        private static BaseShip _protoShip;
        private static Ship _ship;
        public static bool BeginArround { get; set; } = false;
        private static bool _checkValidLocation;
        public static Field Field;
        public static RandomField ShipsOnFild = Field.RandomShips;
        public static void MouseClicked(object sender, MouseEventArgs e)
        {
            var cell = (Cell)sender;            

            if (!BeginArround)
            {
                if (cell.ShipIntoCell == null) return;
                BeginArround = true;

                _ship = cell.ShipIntoCell;
                _ship.Destruction();
                ((RandomFieldAlgorithm)ShipsOnFild).RecalculationCompletingCell(_ship);

                var endLocation = new Location(
                    cell.CellLocation.I - (1 - (int)_ship.Orientation),
                    cell.CellLocation.J - (int)_ship.Orientation);

                DeterminateFieldPlace(_ship.Location,endLocation);

                _protoShip = new BaseShip(cell.CellLocation, _ship.Orientation, _ship.Size);
                _checkValidLocation = ((RandomFieldAlgorithm)ShipsOnFild).IsFreeLocation(_protoShip, _protoShip.Location);
                DeterminatePrototypeShip();

            }
            else
            {
                BeginArround = false;

                Ship newShips;

                _checkValidLocation = ((RandomFieldAlgorithm)ShipsOnFild).IsFreeLocation(_protoShip, _protoShip.Location);

                if (_checkValidLocation)
                {
                    newShips = new Ship(_protoShip.Location, _protoShip.Orientation,
                        _protoShip.Size, Field);
                }
                else
                {
                    newShips = new Ship(_ship);
                    ((RandomFieldAlgorithm)(Field.RandomShips)).AddShips(newShips);
                    Location endedLocation = new Location(
               cell.CellLocation.I + (1 - (int)_protoShip.Orientation) * (_protoShip.Size - 1),
               cell.CellLocation.J + (int)_protoShip.Orientation * (_protoShip.Size - 1));

                    DeterminateFieldPlace(cell.CellLocation, endedLocation);
                }

                ((RandomFieldAlgorithm)ShipsOnFild).AddShips(newShips);

                Location endLocation = new Location(
                    newShips.Location.I +(1 - (int)newShips.Orientation) * (newShips.Size - 1),
                    newShips.Location.J + (int)newShips.Orientation * (newShips.Size - 1)
                    );

                DeterminateFieldPlace(newShips.Location, endLocation);

                _ship = null;
            }
           
        }

        private static void DeterminatePrototypeShip()
        {
            var condition = _checkValidLocation ? CellStatus.ValidLocation : CellStatus.NotValidLocation;

            for (var i=0; i<_protoShip.Size; i++)
            {
                var indexI = _protoShip.Location.I + (1 - (int)_protoShip.Orientation) * i;
                var indexJ = _protoShip.Location.J + (int)_protoShip.Orientation * i;

                if (GeneralFunction.PreventionIndexRange(indexI, indexJ))
                {
                    Field.CellField[indexI, indexJ].CellStatus = condition;
                }
            }
        }

        private static void DeterminateFieldPlace (Location begLocation, Location endLocation)
        {
            for (var i = begLocation.I; i <= endLocation.I; i++)
            {
                for (var j = begLocation.J; j <= endLocation.J; j++)
                {
                    if (!GeneralFunction.PreventionIndexRange(i, j)) continue;
                    var condition = Field.MatrixShips[i, j] ? CellStatus.Completion : CellStatus.Empty;
                    Field.CellField[i, j].CellStatus = condition;
                }                     
            }
        }

        public static void EndedArround()
        {
            BeginArround = false;

            var  newShips = new Ship(_ship);

            ((RandomFieldAlgorithm)ShipsOnFild).AddShips(newShips);

            var endLocation = new Location(
                newShips.Location.I + (1 - (int)newShips.Orientation) * (newShips.Size - 1),
                newShips.Location.J + (int)newShips.Orientation * (newShips.Size - 1)
                );

            DeterminateFieldPlace(newShips.Location, endLocation);

            _ship = null;
        }

        
    }
}
