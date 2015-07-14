using System;
using System.Text.RegularExpressions;

namespace _08.Labyrinth
{
    internal class Labyrinth
    {
        private static void Main(string[] args)
        {
            string[,] matrix = new string[6, 6]
            {
                {"0", "0", "0", "x", "0", "x"},
                {"0", "x", "0", "0", "0", "x"},
                {"0", "*", "x", "0", "x", "0"},
                {"0", "x", "0", "0", "0", "0"},
                {"0", "0", "0", "x", "x", "0"},
                {"0", "0", "0", "x", "0", "x"}
            };


            MarkLabyrinth(matrix, 2, 1, 0);
            MarkUnreachableFields(matrix);
            DrawLabyrinth(matrix);
        }

        public static void DrawLabyrinth(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " | ");
                }

                Console.WriteLine();
            }
        }

        public static void MarkLabyrinth(string[,] matrix, int x, int y, int steps)
        {
            if (IndexInRange(x, matrix.GetLength(0)) && IndexInRange(y, matrix.GetLength(1)))
            {
                if (steps != 0)
                {
                    matrix[x, y] = steps.ToString();
                }

                if (ValidNextMove(matrix, x+1, y, steps))
                {
                    MarkLabyrinth(matrix, x + 1, y, steps + 1);
                }

                if (ValidNextMove(matrix, x, y+1, steps))
                {
                    MarkLabyrinth(matrix, x, y + 1, steps + 1);
                }

                if (ValidNextMove(matrix, x-1, y, steps))
                {
                    MarkLabyrinth(matrix, x - 1, y, steps + 1);
                }

                if (ValidNextMove(matrix, x, y-1, steps))
                {
                    MarkLabyrinth(matrix, x, y - 1, steps + 1);
                }
            }
        }

        public static void MarkUnreachableFields(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == "0")
                    {
                        matrix[i, j] = "u";
                    }
                }
            }
        }

        public static bool IndexInRange(int index, int max)
        {
            if (index >= 0 && index < max)
            {
                return true;
            }

            return false;
        }

        public static bool ValidNextMove(string[,] matrix, int x, int y, int steps)
        {
            if (!IndexInRange(x, matrix.GetLength(0)) || !IndexInRange(y, matrix.GetLength(1)))
            {
                return false;
            }

            if (!Regex.IsMatch(matrix[x, y], @"^\d+$"))
            {
                return false;
            }

            if (!(matrix[x, y] == "0" || Int32.Parse(matrix[x, y]) > steps))
            {
                return false;
            }

            return true;
        }
    }
}