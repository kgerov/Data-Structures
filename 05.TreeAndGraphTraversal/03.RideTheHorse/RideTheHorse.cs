namespace _03.RideTheHorse
{
    using System;
    using System.Collections.Generic;

    class RideTheHorse
    {
        private static int[,] matrix;

        static void Main(string[] args)
        {
            int rows = Int32.Parse(Console.ReadLine());
            int cols = Int32.Parse(Console.ReadLine());
            int startPositionRow = Int32.Parse(Console.ReadLine());
            int startPositionCol = Int32.Parse(Console.ReadLine());

            Point startingPoint = new Point(startPositionRow, startPositionCol);
            matrix = new int[rows, cols];
            matrix[startingPoint.Row, startingPoint.Col] = 1;

            TraverseMatrixBreathFirstSearch(startingPoint);

            PrintMatrixCentralRow();
        }

        public static void TraverseMatrixBreathFirstSearch(Point startingPoint)
        {
            Queue<Point> pointsToTraverse = new Queue<Point>();
            pointsToTraverse.Enqueue(startingPoint);

            while (pointsToTraverse.Count > 0)
            {
                Point currentPoint = pointsToTraverse.Dequeue();
                HashSet<Point> freePointsInRange = GetFreePointsInRange(currentPoint);
                int currentPointValue = matrix[currentPoint.Row, currentPoint.Col];

                foreach (var point in freePointsInRange)
                {
                    matrix[point.Row, point.Col] = currentPointValue + 1;
                    pointsToTraverse.Enqueue(point);
                }
            }
        }

        public static void PrintMatrixCentralRow()
        {
            int middleColumn = matrix.GetLength(1) / 2; //devide by 2 to get middle column

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine(matrix[i, middleColumn]);
            }
        }

        private static HashSet<Point> GetFreePointsInRange(Point pivotPoint)
        {
            // The 8 points below are the 8 possible moves of a horse if is not near the end of a board
            Point topRight = new Point(pivotPoint.Row + 2, pivotPoint.Col + 1);
            Point topLeft = new Point(pivotPoint.Row + 2, pivotPoint.Col - 1);
            Point rightUpper = new Point(pivotPoint.Row + 1, pivotPoint.Col + 2);
            Point rightLower = new Point(pivotPoint.Row - 1, pivotPoint.Col + 2);
            Point leftUpper = new Point(pivotPoint.Row + 1, pivotPoint.Col - 2);
            Point leftLower = new Point(pivotPoint.Row - 1, pivotPoint.Col - 2);
            Point bottomRight = new Point(pivotPoint.Row - 2, pivotPoint.Col + 1);
            Point bottomLeft = new Point(pivotPoint.Row - 2, pivotPoint.Col - 1);

            HashSet<Point> freePointsInRange = new HashSet<Point>()
            {
                topRight, topLeft,
                rightUpper, rightLower,
                leftUpper, leftLower,
                bottomRight, bottomLeft
            };

            // Removes each point that is out of the board or that has been visited
            freePointsInRange.RemoveWhere(x => x.Col < 0 ||
                                      x.Row < 0 ||
                                      x.Row >= matrix.GetLength(0) ||
                                      x.Col >= matrix.GetLength(1) ||
                                      matrix[x.Row, x.Col] != 0);

            return freePointsInRange;
        }
    }
}