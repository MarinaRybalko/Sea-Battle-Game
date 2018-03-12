
namespace GameCore
{
    public class Statistics
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Statistics"/> class
        /// </summary>
        public Statistics()
        {
           
            CountShips = Field.ShipsCount;
            CountLeftShot = Field.Size * Field.Size;
        }
        /// <summary>
        /// Returns or sets amount of ship for player
        /// </summary>
        public int CountShips { set; get; }
        /// <summary>
        /// Returns or sets amount of left shot for player
        /// </summary>
        public int CountLeftShot { get; set; }
    }
}
