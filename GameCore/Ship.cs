
namespace GameCore
{
   
    public class Ship : BaseShip
    {
        private readonly bool[] _crippled;
        private int _shiftDown;
        private int _shiftRight;

        public Field Field { get; set; }
        public Location[] DeckLocation;
        public Ship(Location location, Orientation orientation, int size, Field field) :
            base(location, orientation, size)
        {
            Field = field;

            _crippled = new bool[Size];
            _crippled.Initialize();

            DeckLocation = new Location[Size];

            for (var i = 0; i < Size; i++)
            {
                _shiftDown = i * (1 - (int)Orientation);
                _shiftRight = i * (int)Orientation;

                    Field.MatrixShips[Location.I + _shiftDown, 
                        Location.J + _shiftRight] = true;

                    Field.CellField[Location.I + _shiftDown, 
                        Location.J + _shiftRight].ShipIntoCell = this;

                    DeckLocation[i] =new Location(Location.I + _shiftDown, 
                        Location.J + _shiftRight);               
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
                _shiftDown = i * (1 - (int)Orientation);
                _shiftRight = i * (int)Orientation;


                Field.MatrixShips[Location.I + _shiftDown, 
                        Location.J+_shiftRight] = false;

                Field.CellField[Location.I + _shiftDown, 
                        Location.J + _shiftRight].ShipIntoCell = null;
            }
        }

        public bool CheckDrowned(Location location)
        {
            bool check = true;

            for (var i = 0; i < Size; i++)
            {
                if ((location.I == DeckLocation[i].I) &&
                    (location.J == DeckLocation[i].J))
                {
                    _crippled[i] = true;
                    break;
                }
            }

            foreach (var value in _crippled)
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
                if ((location.I == DeckLocation[i].I) &&
                    (location.J == DeckLocation[i].J))
                { check = true; break; }
            }

            return check;
        }

        public void MarkShip(bool[,] matrixSips)
        {
            for (var i = DeckLocation[0].I - 1; i <= DeckLocation[Size - 1].I + 1; i++)
            {
                for (var j = DeckLocation[0].J - 1; j <= DeckLocation[Size - 1].J + 1; j++)
                {
                    if (GeneralFunction.PreventionIndexRange(i, j)) matrixSips[i, j] = true;
                }
            }
        }
      
    }
}
