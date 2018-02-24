using System;

namespace GameCore
{
   public class ShotEventArgs:EventArgs
    {
        public readonly CellStatus ShotResult;

        public ShotEventArgs(CellStatus result)
        {
            ShotResult = result; 
        } 
    }
}
