
namespace GameCore
{
    public class BaseShip
    {
        public int Size { get; set; }


        public Orientation Orientation { get; set; }

        public Location Location { get; set; }

        public BaseShip(Location location,Orientation orientation, int size)
        {
            Location = location;
            Orientation = orientation;
            Size = size;
        }

        public BaseShip()
        { }
    }
}
