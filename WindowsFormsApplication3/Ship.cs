using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleGame
{
    public enum Orientation
    {
        Vertical,
        Horizontal
    }
    public class Ship : BaseShip
    {
        bool[] crippled;

        public Field Field { get; set; }

        public Location[] deckLocation;

        int shiftDown;
        int shiftRight;

        public Ship(Location location, Orientation orientation, int size, Field field) :
            base(location, orientation, size)
        {
            Field = field;

            crippled = new bool[Size];
            crippled.Initialize();

            deckLocation = new Location[Size];

            for (var i = 0; i < Size; i++)
            {
                shiftDown = i * (1 - (int)Orientation);
                shiftRight = i * (int)Orientation;

                    Field.MatrixShips[Location.I + shiftDown, 
                        Location.J + shiftRight] = true;

                    Field.CellField[Location.I + shiftDown, 
                        Location.J + shiftRight].ShipIntoCell = this;

                    deckLocation[i] =new Location(Location.I + shiftDown, 
                        Location.J + shiftRight);               
            }
        }

        public Ship(Ship ship):
            this(ship.Location, ship.Orientation, ship.Size, ship.Field)
        {
          
        }
   
        public void Destruction()
        {
            for (var i = 0; i < Size; i++)
            {
                shiftDown = i * (1 - (int)Orientation);
                shiftRight = i * (int)Orientation;


                Field.MatrixShips[Location.I + shiftDown, 
                        Location.J+shiftRight] = false;

                Field.CellField[Location.I + shiftDown, 
                        Location.J + shiftRight].ShipIntoCell = null;
            }
        }
        //TODO MARK CELLS NEAR THE WRECKED SHIP
        public bool CheckDrowned(Location location)
        {
            bool check = true;

            for (var i = 0; i < Size; i++)
            {
                if ((location.I == deckLocation[i].I) && 
                    (location.J == deckLocation[i].J))
                {
                    crippled[i] = true;
                    break;
                }
            }

            foreach (var value in crippled)
            {
                if (!value) check = false;
            }

            return check;
        }

        public bool ChekShip(Location location)
        {
            bool check = false;

            for (var i = 0; i < Size; i++)
            {
                if ((location.I == deckLocation[i].I) &&
                    (location.J == deckLocation[i].J))
                { check = true; break; }
            }

            return check;
        }

        public void MarkShip(bool[,] MatrixSips)
        {
            for (var i = deckLocation[0].I - 1; i <= deckLocation[Size - 1].I + 1; i++)
            {
                for (var j = deckLocation[0].J - 1; j <= deckLocation[Size - 1].J + 1; j++)
                {
                    if (GeneralFunction.PreventionIndexRange(i, j)) MatrixSips[i, j] = true;
                }
            }
        }
      
    }
}
