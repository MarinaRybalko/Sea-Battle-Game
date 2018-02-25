using System;
using System.Collections.Generic;


namespace GameCore
{
    public class RandomField
    {
        protected static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public List<Ship> ListShips = new List<Ship>();
        protected  readonly bool[,] CompletingCell = new bool[Field.Size, Field.Size];
        protected readonly bool[,] NotvalidCellLocation = new bool[Field.Size, Field.Size];
        protected int ValidCell;
        protected  readonly BaseShip Prototype = new BaseShip();
        protected int OneDimencional;
        protected Field Field;
        public RandomField(Field field)
        {
            Field = field;
           
        }
    }
}
