

namespace GameCore
{
    public struct Location
    {
        /// <summary>
        /// Returns or sets coordinates I
        /// </summary>
        public int I
        {
            get; set;
        }
        /// <summary>
        /// Returns or sets coordinates J
        /// </summary>
        public int J
        {
            get; set;
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="Location"/> class
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public Location(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
