
namespace GameCore
{
    public class BaseShip
    {
        int size;
       

        public int Size
        {
            get { return size; }
            set { size = value; }
        }


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
