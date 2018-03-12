
namespace GameCore
{
    public static class GeneralFunction
    {
        /// <summary>
        /// Translates random number into cell location
        /// </summary>
        /// <param name="matrixLocation"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Location FromNumberToLocation(bool[,] matrixLocation, int  number)
        {
            var count = 0;

            var newLocation = new Location();

            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (matrixLocation[i, j]) continue;
                    if (count == number)
                    {
                        matrixLocation[i, j] = true;
                        newLocation.I = i;
                        newLocation.J = j;
                    }
                    count++;
                }
            }

            return newLocation;
        }
        /// <summary>
        /// Checks cell location for correctness of coordinates values
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static bool PreventionIndexRange(int i, int j)
        {
            return ((i > -1) && (j > -1) && (i < Field.Size) && (j < Field.Size));
        }
        /// <summary>
        /// Fills matrix with "false" values
        /// </summary>
        /// <param name="matrix"></param>
        public static void FalseToMatrix(bool[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = false;
                   
                }
            }
        }
    }
}
