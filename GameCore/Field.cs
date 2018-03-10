using System;


namespace GameCore
{
   public class Field
   {
        /// <summary>
        /// Returns or sets field size
        /// </summary>
        public static int Size { get; } = 10;
        /// <summary>
        /// Returns or sets field cells
        /// </summary>
        public Cell [,] CellField { get; set; }
        /// <summary>
        /// Returns or sets matrix that describes presence or absence of a ship in a cell
        /// </summary>
        public bool[,] MatrixShips { get; set; }
        /// <summary>
        /// Occurs if the player makes a shot
        /// </summary>
        public event EventHandler MadeShot;
        /// <summary>
        /// Returns or sets amount of ships
        /// </summary>
        public static int ShipsCount { get; } = (int)ShipType.FourdeckShips + (int)ShipType.ThreedeckShips + (int)ShipType.TwodeckShips + (int)ShipType.OnedeckShips;
        /// <summary>
        /// Returns or sets ship arrangement on the field
        /// </summary>
        public RandomField RandomShips { get; set; }
        /// <summary>
        /// Initialize a new instance of the <see cref="Field"/> by default
        /// </summary>
        public Field()
        {
            MatrixShips = new bool[Size, Size];
            MatrixShips.Initialize();
           
            CellField = new Cell[Size, Size];

            for (var  i = 0; i<Size ; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    CellField[i, j] = new Cell {CellLocation = new Location(i, j)};
                    MatrixShips[i, j] = false;                 
                }
            }

        }

        /// <summary>
        /// Marks completion cells on field
        /// </summary>
        public void DisplayCompletionCell()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    CellField[i, j].CellStatus = MatrixShips[i, j] ? CellStatus.Completion : CellStatus.Empty;
                }
            }
        }

        private void DrownedShip(BaseShip ship)
        {
            var location = ship.Location;
            var orientation = ship.Orientation;
            var size = ship.Size;

            if(orientation== Orientation.Vertical)
            { 
                    if (location.I - 1 >= 0)
                    {
                        if (location.J + 1 < 10)
                            CellField[location.I - 1, location.J + 1].CellStatus = CellStatus.Miss;

                        CellField[location.I - 1, location.J].CellStatus = CellStatus.Miss;

                        if (location.J - 1 >= 0)
                            CellField[location.I - 1, location.J - 1].CellStatus = CellStatus.Miss;

                    }

                    if (location.I + ship.Size < 10)
                    {
                        if (location.J + 1 < 10)
                            CellField[location.I + ship.Size, location.J + 1].CellStatus = CellStatus.Miss;

                        CellField[location.I + ship.Size, location.J].CellStatus = CellStatus.Miss;

                        if (location.J - 1 >= 0)
                            CellField[location.I + ship.Size, location.J - 1].CellStatus = CellStatus.Miss;
                    }
            }

            else
            {

                    if (location.J + ship.Size < 10)
                    {
                        if (location.I + 1 < 10)

                            CellField[location.I + 1, location.J + ship.Size].CellStatus = CellStatus.Miss;

                        CellField[location.I, location.J + ship.Size].CellStatus = CellStatus.Miss;

                        if (location.I - 1 >= 0)
                            CellField[location.I - 1, location.J + ship.Size].CellStatus = CellStatus.Miss;
                    }

                    if (location.J - 1 >= 0)
                    {
                        if (location.I + 1 < 10)
                            CellField[location.I + 1, location.J - 1].CellStatus = CellStatus.Miss;
                        if (location.I - 1 >= 0)
                            CellField[location.I - 1, location.J - 1].CellStatus = CellStatus.Miss;

                        CellField[location.I, location.J - 1].CellStatus = CellStatus.Miss;
                    }
            }

            for (var i = 0; i < size; i++)
            {
                var shiftDown = i * (1 - (int)orientation);
                var shiftRight = i * (int)orientation;

                CellField[location.I + shiftDown, location.J + shiftRight].CellStatus =
                    CellStatus.Drowned;

                if (ship.Orientation == Orientation.Horizontal)
                {
                    if (location.I - 1 >= 0)

                        CellField[location.I - 1 + shiftDown, location.J + shiftRight].CellStatus = CellStatus.Miss;
                    if (location.I + 1 < 10)
                        CellField[location.I + 1 + shiftDown, location.J + shiftRight].CellStatus = CellStatus.Miss;

                }
                else
                {


                    if (location.J + 1 < 10)
                        CellField[location.I + shiftDown, location.J + 1 + shiftRight].CellStatus = CellStatus.Miss;
                    if (location.J - 1 >= 0)
                        CellField[location.I + shiftDown, location.J - 1 + shiftRight].CellStatus = CellStatus.Miss;
                }

            }
        }

        /// <summary>
        /// Performs and checks players shot
        /// </summary>
        /// <param name="cellShot"></param>
        /// <returns></returns>
        public CellStatus Shot(Cell cellShot)
        {
        
            if (MatrixShips[cellShot.CellLocation.I, cellShot.CellLocation.J])
            {
               
                if (cellShot.ShipIntoCell.CheckDrowned(cellShot.CellLocation))
                {
                    DrownedShip(cellShot.ShipIntoCell);
                }
                else
                {
                    cellShot.CellStatus = CellStatus.Crippled;
                }
            }
            else
            {
                cellShot.CellStatus = CellStatus.Miss;
            }

            MadeShot?.Invoke(this, new ShotEventArgs(cellShot.CellStatus));
            return cellShot.CellStatus;
        }


    }
}

