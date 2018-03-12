
namespace GameCore
{
    public class BaseShip
    {
        /// <summary>
        /// Returns or sets ship size
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Returns or sets ship orientation
        /// </summary>
        public Orientation Orientation { get; set; }
        /// <summary>
        /// Returns or sets ship location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// Initialize a new instance of the <see cref="BaseShip"/> class
        /// </summary>
        /// <param name="location"></param>
        /// <param name="orientation"></param>
        /// <param name="size"></param>
        public BaseShip(Location location,Orientation orientation, int size)
        {
            Location = location;
            Orientation = orientation;
            Size = size;
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="BaseShip"/> class by default
        /// </summary>
        public BaseShip()
        { }
    }
}
