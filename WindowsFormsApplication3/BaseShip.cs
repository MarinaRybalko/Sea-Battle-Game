using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    public class BaseShip
    {
        int size;
        Orientation shipOrientation;
        Location shipLocation;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public Orientation Orientation
        {
            get { return shipOrientation; }
            set { shipOrientation = value; }
        }

        public Location Location
        {
            get { return shipLocation; }
            set { shipLocation = value; }
        }

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
