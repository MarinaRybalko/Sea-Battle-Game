using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    public class RandomShips
    {
        static Random random = new Random(DateTime.Now.Millisecond);

        public List<Ship> ListShips = new List<Ship>();

        bool[,] completingCell = new bool [Field.Size,Field.Size];
        bool[,] notvalidCellLocation = new bool[Field.Size,Field.Size];

        Field field;

        int validCell;

        BaseShip prototype=new BaseShip();
        
        int oneDimencional;


        public RandomShips(Field field)
        {
            this.field = field;
            NewArrangement();
        }

        void NewArrangement()
        {
            GeneralFunction.FalseToMatrix(completingCell);

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < i + 1; j++)
                {               
                    prototype.Orientation = (Orientation)random.Next(0, 2);
                    prototype.Size = 4 - i;

                    CountingValidCell(prototype);
                    oneDimencional = random.Next(0, validCell);

                    prototype.Location =
                        GeneralFunction.FromNumberToLocation(notvalidCellLocation, oneDimencional);
               
                    ListShips.Add(new Ship(prototype.Location, prototype.Orientation,
                        prototype.Size, field));

                    ListShips[ListShips.Count - 1].MarkShip(completingCell);
                }
            }
        }

        void CountingValidCell(BaseShip prototype)
        {
            GeneralFunction.FalseToMatrix(notvalidCellLocation);

            validCell = 0;
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (CheckLocation(prototype, new Location(i, j))) validCell++;
                }
            }
        }

        public bool CheckLocation(BaseShip prototype, Location location)
        {
            int dj = 0;
            int di = 0;

            if (prototype.Orientation == Orientation.Horizontal) dj = 1;
            if (prototype.Orientation == Orientation.Vertical) di = 1;

            for (var i = 0; i<prototype.Size; i++)
            {
                if ((!GeneralFunction.PreventionIndexRange(location.I + i * di, location.J + i * dj)) ||(completingCell[location.I + i * di, location.J + i * dj]))
                    {
                        notvalidCellLocation[location.I, location.J] = true;
                        return false;
                    }
            }

            return true;
        }

        public void RecalculationCompletingCell(Ship ship)
        {
            ListShips.Remove(ship);
            GeneralFunction.FalseToMatrix(completingCell);
           
            foreach (var value in ListShips) value.MarkShip(completingCell);            
        }

        public void AddShips(Ship ship)
        {
            ListShips.Add(ship);
            ship.MarkShip(completingCell);
        }

        public void RandomArrangement()
        {

            foreach (var value in field.RandomShips.ListShips)
                value.Destruction();

            field.RandomShips.ListShips.Clear();

            NewArrangement();
        }

        public void RandomClicked(object sender, EventArgs e)
        {
            RandomArrangement();
            Form1.LeftField.DisplayCompletionCell();
        }
    }
}
