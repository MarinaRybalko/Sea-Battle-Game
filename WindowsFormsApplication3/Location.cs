using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    public struct Location
    {
        public int I
        {
            get; set;
        }

        public int J
        {
            get; set;
        }

        public Location(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
