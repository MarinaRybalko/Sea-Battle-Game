using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleGame
{
    public static class MouseEvent
    {
        static BaseShip protoShip = null;
        static Ship ship = null;

        public static bool BeginArround { get; set; } = false;
        static bool checkValidLocation = false;

        static Field field = Form1.LeftField;
        static RandomShips shipsOnFild = field.RandomShips;

        public static void MouseClicked(object sender, MouseEventArgs e)
        {          
                SeaBattlePicture cell = (SeaBattlePicture)sender;            

            if (!BeginArround)
            {
                if (cell.ShipIntoCell == null) return;
                BeginArround = true;

                ship = cell.ShipIntoCell;
                ship.Destruction();
                shipsOnFild.RecalculationCompletingCell(ship);

                Location endLocation = new Location(
                    cell.CellLocation.I - (1 - (int)ship.Orientation),
                    cell.CellLocation.J - (int)ship.Orientation);

                DisplayFieldPlace(ship.Location,endLocation);

                protoShip = new BaseShip(cell.CellLocation, ship.Orientation, ship.Size);
                checkValidLocation = shipsOnFild.CheckLocation(protoShip, protoShip.Location);
                DisplayPrototypeShip();

            }
            else
            {
                BeginArround = false;

                Ship newShips;

                checkValidLocation = shipsOnFild.CheckLocation(protoShip, protoShip.Location);

                if (checkValidLocation)
                {
                    newShips = new Ship(protoShip.Location, protoShip.Orientation,
                        protoShip.Size, Form1.LeftField);
                }
                else
                {
                    newShips = new Ship(ship);

                    Location endedLocation = new Location(
               cell.CellLocation.I + (1 - (int)protoShip.Orientation) * (protoShip.Size - 1),
               cell.CellLocation.J + (int)protoShip.Orientation * (protoShip.Size - 1));

                    DisplayFieldPlace(cell.CellLocation, endedLocation);
                }

                shipsOnFild.AddShips(newShips);

                Location endLocation = new Location(
                    newShips.Location.I +(1 - (int)newShips.Orientation) * (newShips.Size - 1),
                    newShips.Location.J + (int)newShips.Orientation * (newShips.Size - 1)
                    );

                DisplayFieldPlace(newShips.Location, endLocation);

                ship = null;
            }
           
        }

        static void DisplayPrototypeShip()
        {
            CellStatus condition;

            if (checkValidLocation)
            {
                condition = CellStatus.ValidLocation;
            }
            else
            {
                condition = CellStatus.NotValidLocation;
            }

            int indexI;
            int indexJ;
             
            for (var i=0; i<protoShip.Size; i++)
            {
                indexI = protoShip.Location.I + (1 - (int)protoShip.Orientation) * i;
                indexJ = protoShip.Location.J + (int)protoShip.Orientation * i;

                if (GeneralFunction.PreventionIndexRange(indexI, indexJ))
                {
                    field.CellField[indexI, indexJ].RenderingMode = condition;
                }
            }
        }

        static void DisplayFieldPlace (Location begLocation, Location endLocation)
        {
            CellStatus condition;

            for (var i = begLocation.I; i <= endLocation.I; i++)
            {
                for (var j = begLocation.J; j <= endLocation.J; j++)
                {
                    if (GeneralFunction.PreventionIndexRange(i, j))
                    { 
                        if (field.MatrixShips[i, j])
                        {
                            condition = CellStatus.Completion;
                        }
                        else
                        {
                            condition = CellStatus.Empty;
                        }
                        field.CellField[i, j].RenderingMode = condition;
                    }
                }                     
            }
        }

        public static void EndedArround()
        {
            BeginArround = false;

            Ship  newShips = new Ship(ship);

            field.RandomShips.AddShips(newShips);

            Location endLocation = new Location(
                newShips.Location.I + (1 - (int)newShips.Orientation) * (newShips.Size - 1),
                newShips.Location.J + (int)newShips.Orientation * (newShips.Size - 1)
                );

            DisplayFieldPlace(newShips.Location, endLocation);

            ship = null;
        }
    }
}
