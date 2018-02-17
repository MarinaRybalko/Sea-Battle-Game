using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SeaBattleGame
{
   public class Field
   {
        static public int Size { get; } = 10;
       
        public SeaBattlePicture[,] CellField { get; set; }

        public bool[,] MatrixShips { get; set; }

        public event EventHandler MadeShot;

        public static int FourdeckShips { get; } = 1;
        public static int ThreedeckShips { get; } = 2;
        public static int TwodeckShips { get; } = 3;
        public static int OnedeckShips { get; } = 4;

        public static int ShipsCount { get; } = 
            FourdeckShips + ThreedeckShips + 
            TwodeckShips + OnedeckShips;

        public RandomShips RandomShips { get; set; }

        public Field()
        {
            MatrixShips = new bool[Size, Size];
            MatrixShips.Initialize();
           
            CellField = new SeaBattlePicture[Size, Size];

            for (var  i = 0; i<Size ; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    CellField[i, j] = new SeaBattlePicture();
                    CellField[i, j].CellLocation = new Location(i, j);
                    MatrixShips[i, j] = false;                 
                }
            }

            FillingShips();   
        }

        void FillingShips()
        {
            RandomShips = new RandomShips(this);
        }

        public void DisplayCompletionCell()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (MatrixShips[i, j])
                    {
                        CellField[i, j].RenderingMode = CellStatus.Completion;
                    }
                    else
                    {
                        CellField[i, j].RenderingMode = CellStatus.Empty;
                    }
                }
            }
        }

        void DrownedShip(Ship ship)
        {
            Location location = ship.Location;
            Orientation orientation = ship.Orientation;
            int size = ship.Size;

            int shiftDown = 0;
            int shiftRight = 0;

            for (var i = 0; i < size; i++)
            {
                shiftDown = i * (1 - (int)orientation);
                shiftRight = i * (int)orientation;

                CellField[location.I + shiftDown, location.J + shiftRight].RenderingMode =
                    CellStatus.Drowned;
              

            }
        }
        

        public CellStatus Shot(SeaBattlePicture cellShot)
        {
            if (MatrixShips[cellShot.CellLocation.I, cellShot.CellLocation.J])
            {
                if (cellShot.ShipIntoCell.CheckDrowned(cellShot.CellLocation))
                {
                    DrownedShip(cellShot.ShipIntoCell);
                }
                else
                {
                    cellShot.RenderingMode = CellStatus.Crippled;
                }
            }
            else
            {
                cellShot.RenderingMode = CellStatus.Miss;
            }

            MadeShot(this, new shotEventArgs(cellShot.RenderingMode));
            return cellShot.RenderingMode;
        }  
   }
}

