namespace _03.Calculator
{
    using System;

    class Tester
    {
        static void Main(string[] args)
        {
            string exressionTestOne = "5 + 6";
            Console.WriteLine(Calculator.CalculateExpression(exressionTestOne));

            string expressionTestTwo = "( 2 + 3 ) * 4.5";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestTwo));

            string expressionTestThree = "2 + 3 * 1.5 - 1";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestThree));

            string expressionTestFour = "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestFour));

            string expressionTestFive = "5 + ( ( 1 + 2 ) * 4 ) - 3";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestFive));

            string expressionTestSix = "1.5 - 2.5 * 2 * ( -3 )";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestSix));

            string expressionTestSeven = "1 / 2";
            Console.WriteLine(Calculator.CalculateExpression(expressionTestSeven));
        }
    }
}
