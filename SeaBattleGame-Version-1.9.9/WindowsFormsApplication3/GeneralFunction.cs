using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    public class GeneralFunction
    {
        public static Location FromNumberToLocation(bool[,] matrixLocation, int  number)
        {
            int count = 0;

            Location newLocation = new Location();

            for (var i = 0; i < Field.Size; i++)
            {
                for (var j = 0; j < Field.Size; j++)
                {
                    if (!matrixLocation[i, j])
                    {
                        if (count == number)
                        {
                            matrixLocation[i, j] = true;
                            newLocation.I = i;
                            newLocation.J = j;
                        }
                        count++;
                    }
                }
            }

            return newLocation;
        }

        public static bool PreventionIndexRange(int i, int j)
        {
            return ((i > -1) && (j > -1) && (i < Field.Size) && (j < Field.Size));
        }

        public static void FalseToMatrix(bool[,] Matrix)
        {
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = false;
                   
                }
            }
        }
    }
}
