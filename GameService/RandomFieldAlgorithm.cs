using System;
using GameCore;

namespace GameService
{
    public class RandomFieldAlgorithm: RandomField
    {
        private void CountingValidCell(BaseShip prototype)
        {
            GeneralFunction.FalseToMatrix(NotvalidCellLocation);

            ValidCell = 0;
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (IsFreeLocation(prototype, new Location(i, j))) ValidCell++;
                }
            }
        }
        public  void NewArrangement(Field field)
        {
            GeneralFunction.FalseToMatrix(CompletingCell);

            for (var i = 0; i < Enum.GetValues(typeof(Direction)).Length; i++)
            {
                for (var j = 0; j < i + 1; j++)
                {               
                    Prototype.Orientation = (Orientation)Random.Next(0, Enum.GetValues(typeof(Orientation)).Length);
                    Prototype.Size = Enum.GetValues(typeof( Direction)).Length - i;

                    CountingValidCell(Prototype);
                    OneDimencional = Random.Next(0, ValidCell);

                    Prototype.Location =
                        GeneralFunction.FromNumberToLocation(NotvalidCellLocation, OneDimencional);
               
                    ListShips.Add(new Ship(Prototype.Location, Prototype.Orientation,
                        Prototype.Size, field));

                    ListShips[ListShips.Count - 1].MarkShip(CompletingCell);
                }
            }
        }   
        public bool IsFreeLocation(BaseShip prototype, Location location)
        {
            var dj = 0;
            var di = 0;

            switch (prototype.Orientation)
            {
                case Orientation.Horizontal:
                    dj = 1;
                    break;
                case Orientation.Vertical:
                    di = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i<prototype.Size; i++)
            {
                if ((GeneralFunction.PreventionIndexRange(location.I + i * di, location.J + i * dj)) &&
                    (!CompletingCell[location.I + i * di, location.J + i * dj])) continue;
                NotvalidCellLocation[location.I, location.J] = true;
                return false;
            }

            return true;
        }
        public  void RecalculationCompletingCell(Ship ship)
        {
            ListShips.Remove(ship);
            GeneralFunction.FalseToMatrix(CompletingCell);
           
            foreach (var value in ListShips) value.MarkShip(CompletingCell);            
        }
        public void AddShips(Ship ship)
        {
            ListShips.Add(ship);
            ship.MarkShip(CompletingCell);
        }
        public RandomFieldAlgorithm(Field field) : base(field)
        {
            NewArrangement(field);
        }
    }
}
