
namespace GameCore
{
    public class Statistics
    {
        public Statistics()
        {
           
            CountShips = Field.ShipsCount;
            CountLeftShot = Field.Size * Field.Size;
        }

        public int CountShips { set; get; }

        public int CountLeftShot { get; set; }
    }
}
