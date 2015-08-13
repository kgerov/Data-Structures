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
        }
    }
}
