using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    class shotEventArgs:EventArgs
    {
        public readonly CellStatus shotResult;

        public shotEventArgs(CellStatus result)
        {
            shotResult = result; 
        } 
    }
}
