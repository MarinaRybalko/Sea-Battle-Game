using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class RandomField
    {
        protected static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public List<Ship> ListShips = new List<Ship>();
        protected static readonly bool[,] CompletingCell = new bool[Field.Size, Field.Size];
        protected static readonly bool[,] NotvalidCellLocation = new bool[Field.Size, Field.Size];
        protected static int _validCell;
        protected static readonly BaseShip Prototype = new BaseShip();
        protected static int _oneDimencional;
        Field field;
        public RandomField(Field field)
        {
            this.field = field;
           
        }
    }
}
