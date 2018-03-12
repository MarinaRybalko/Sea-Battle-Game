using System;
using System.Collections.Generic;


namespace GameCore
{
    public class RandomField
    {
        protected static readonly Random Random = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// Returns or sets list of ships
        /// </summary>
        public List<Ship> ListShips = new List<Ship>();
        protected  readonly bool[,] CompletingCell = new bool[Field.Size, Field.Size];
        protected readonly bool[,] NotvalidCellLocation = new bool[Field.Size, Field.Size];
        protected int ValidCell;
        protected  readonly BaseShip Prototype = new BaseShip();
        protected int OneDimencional;
        protected Field Field;
        /// <summary>
        /// Initialize a new instance of the Field <see cref="RandomField"/> class
        /// </summary>
        /// <param name="field"></param>
        public RandomField(Field field)
        {
            Field = field;
           
        }
    }
}
