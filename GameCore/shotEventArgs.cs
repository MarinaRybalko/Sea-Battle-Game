using System;

namespace GameCore
{
   public class ShotEventArgs:EventArgs
    {
        /// <summary>
        /// Returns or sets shot result
        /// </summary>
        public readonly CellStatus ShotResult;
        /// <summary>
        /// Initialize a new instance of the <see cref="ShotEventArgs"/> class
        /// </summary>
        /// <param name="result"></param>
        public ShotEventArgs(CellStatus result)
        {
            ShotResult = result; 
        } 
    }
}
