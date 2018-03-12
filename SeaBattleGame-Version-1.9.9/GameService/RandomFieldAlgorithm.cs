using System;
using GameCore;
using NLog;

namespace GameService
{
    public class RandomFieldAlgorithm: RandomField
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private void CountingValidCell(BaseShip prototype)
        {
            Logger.Debug("Mark field cells available");
            GeneralFunction.FalseToMatrix(NotvalidCellLocation);
            Logger.Debug("Mark field cells available successfully completed");
            ValidCell = 0;
            Logger.Debug("Attempt to count free locations "+ValidCell);
            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (IsFreeLocation(prototype, new Location(i, j))) ValidCell++;
                }
            }
            Logger.Debug("Attempt to count free locations successfully completed "+ ValidCell);
        }
        /// <summary>
        /// Sets new ships arrangement
        /// </summary>
        /// <param name="field"></param>
        public  void NewArrangement(Field field)
        {
            Logger.Debug("Mark field cells available");
            GeneralFunction.FalseToMatrix(CompletingCell);
            Logger.Debug("Mark field cells available successfully completed");
           
            for (var i = 0; i < Enum.GetValues(typeof(Direction)).Length; i++)
            {
                for (var j = 0; j < i + 1; j++)
                {
                    Logger.Debug("Attempt to get random ship orientation and random ship size");
                    Prototype.Orientation = (Orientation)Random.Next(0, Enum.GetValues(typeof(Orientation)).Length);                   
                    Prototype.Size = Enum.GetValues(typeof( Direction)).Length - i;
                    Logger.Debug("Attempt to get random ship size and get random ship orientation successfully completed. Size is " + Prototype.Size+"orientation is: "+Prototype.Orientation);
                    CountingValidCell(Prototype);
                    OneDimencional = Random.Next(0, ValidCell);
                    Logger.Debug("Attempt to get ship location");
                    Prototype.Location = GeneralFunction.FromNumberToLocation(NotvalidCellLocation, OneDimencional);
                    Logger.Debug("Attempt to get ship location successfully completed. Location is "+Prototype.Location);
                    Logger.Debug("Attempt to add and mark ship");
                    ListShips.Add(new Ship(Prototype.Location, Prototype.Orientation,
                        Prototype.Size, field));
                    ListShips[ListShips.Count - 1].MarkShip(CompletingCell);
                    Logger.Debug("Attempt to add and mark ship successfully completed. Ship count " + ListShips.Count);
                }
            }
        }   
        /// <summary>
        /// Checks whether the required location is free
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsFreeLocation(BaseShip prototype, Location location)
        {
            var dj = 0;
            var di = 0;
            Logger.Debug("Attempt to get prospective direction");
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
            Logger.Debug("Attempt to get prospective direction successfully completed. Direction is " + di+" "+dj );
            for (var i = 0; i<prototype.Size; i++)
            {
                if ((GeneralFunction.PreventionIndexRange(location.I + i * di, location.J + i * dj)) &&
                    (!CompletingCell[location.I + i * di, location.J + i * dj])) continue;
                Logger.Debug("Attempt to mark cell location like not valid");
                NotvalidCellLocation[location.I, location.J] = true;
                return false;
            }
            Logger.Debug("Attempt to mark cell location like not valid successfully completed");
            return true;
        }
        /// <summary>
        /// Recalculates completing ship cells 
        /// </summary>
        /// <param name="ship"></param>
        public  void RecalculationCompletingCell(Ship ship)
        {
            Logger.Debug("Attempt to remove ship from field and mark field cells available. Ship count " + ListShips.Count);
            ListShips.Remove(ship);
            GeneralFunction.FalseToMatrix(CompletingCell);
            Logger.Debug("Attempt to remove ship from field and mark field cells available successfully completed.Ship count " + ListShips.Count);
            Logger.Debug("Attempt to mark ship");
            foreach (var value in ListShips) value.MarkShip(CompletingCell);
            Logger.Debug("Attempt to mark ship successfully completed");
        }
        /// <summary>
        /// Adds an ship to the end of list
        /// </summary>
        /// <param name="ship"></param>
        public void AddShips(Ship ship)
        {
            Logger.Debug("Attempt to add ship onto field and mark it. Ship count " + ListShips.Count);
            ListShips.Add(ship);
            ship.MarkShip(CompletingCell);
            Logger.Debug("Attempt to add ship onto field and mark it successfully completed.Ship count " + ListShips.Count);
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="RandomFieldAlgorithm"/> class
        /// </summary>
        /// <param name="field"></param>
        public RandomFieldAlgorithm(Field field) : base(field)
        {
            Logger.Debug("Attempt to generate new random feet");
            NewArrangement(field);
            Logger.Debug("Attempt to generate new random feet successfully completed");
        }
    }
}
