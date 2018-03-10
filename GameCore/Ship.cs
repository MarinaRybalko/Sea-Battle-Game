
namespace GameCore
{
   
    public class Ship : BaseShip
    {
        private readonly bool[] _crippled;
        private int _shiftDown;
        private int _shiftRight;
        /// <summary>
        /// Returns or sets field for ship
        /// </summary>
        public Field Field { get; set; }
        /// <summary>
        /// Returns or sets array of deck locations for ship
        /// </summary>
        public Location[] DeckLocation;
        /// <summary>
        /// Initialize a new instance of the <see cref="Ship"/> class
        /// </summary>
        /// <param name="location"></param>
        /// <param name="orientation"></param>
        /// <param name="size"></param>
        /// <param name="field"></param>
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
        /// <summary>
        /// Initialize a new instance of the <see cref="Ship"/> class
        /// </summary>
        /// <param name="ship"></param>
        public Ship(Ship ship):
            this(ship.Location, ship.Orientation, ship.Size, ship.Field)
        {
          
        }
        /// <summary>
        /// Destructs ships
        /// </summary>
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
        /// <summary>
        /// Checks is ship drowned
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check is ship in cell
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Marks matrix cells like completion
        /// </summary>
        /// <param name="matrixShips"></param>
        public void MarkShip(bool[,] matrixShips)
        {
            for (var i = DeckLocation[0].I - 1; i <= DeckLocation[Size - 1].I + 1; i++)
            {
                for (var j = DeckLocation[0].J - 1; j <= DeckLocation[Size - 1].J + 1; j++)
                {
                    if (GeneralFunction.PreventionIndexRange(i, j)) matrixShips[i, j] = true;
                }
            }
        }
      
    }
}
