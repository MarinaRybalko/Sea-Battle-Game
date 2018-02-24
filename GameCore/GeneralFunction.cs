
namespace GameCore
{
    public static class GeneralFunction
    {
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

        public static bool PreventionIndexRange(int i, int j)
        {
            return ((i > -1) && (j > -1) && (i < Field.Size) && (j < Field.Size));
        }

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
