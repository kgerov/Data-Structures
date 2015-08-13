namespace _03.Calculator
{
    using CalculatorHelpers;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Calculator
    {
        private static readonly List<Operator> supportedOperations;

        static Calculator()
        {
            supportedOperations = new List<Operator>()
            {
                new Operator('+', Precedence.Low, Assosiativity.Left),
                new Operator('-', Precedence.Low, Assosiativity.Left),
                new Operator('*', Precedence.Medium, Assosiativity.Left),
                new Operator('/', Precedence.Medium, Assosiativity.Left),
                new Operator('^', Precedence.High, Assosiativity.Right),
            };
        }

        public static decimal CalculateExpression(string expression)
        {
            Queue<string> notation = ConvertExpressionToNotation(expression);
            Console.WriteLine(String.Join(" ", notation));
            //decimal expressionValue = ParseNotation(notation);

            return -1;
            //return expressionValue;
        }

        private static Queue<string> ConvertExpressionToNotation(string expression)
        {
            string[] expressionSymbols = expression.Split(' ');
            Queue<string> reversePolishNotation = new Queue<string>();
            Stack<string> operatorsCollection = new Stack<string>();

            for (int i = 0; i < expressionSymbols.Length; i++)
            {
                string currentSymbol = expressionSymbols[i];

                bool isNumeric = IsNumeric(currentSymbol);
                bool isOperator = IsOperator(currentSymbol);
                bool isParenthesis = IsParenthesis(currentSymbol);

                if (isNumeric)
                {
                    reversePolishNotation.Enqueue(currentSymbol);
                }
                else if (isOperator)
                {
                    if (operatorsCollection.Count == 0)
                    {
                        operatorsCollection.Push(currentSymbol);
                    }
                    else
                    {
                        string lastInsertedOperator = operatorsCollection.Peek();
                        bool shouldAddOperatorToNotation = ShouldAddOperatorToNotation(lastInsertedOperator, currentSymbol);

                        while (shouldAddOperatorToNotation && operatorsCollection.Count != 0)
                        {
                            reversePolishNotation.Enqueue(lastInsertedOperator);
                            operatorsCollection.Pop();
                            if (operatorsCollection.Count == 0)
                            {
                                break;
                            }

                            lastInsertedOperator = operatorsCollection.Peek();
                            shouldAddOperatorToNotation = ShouldAddOperatorToNotation(lastInsertedOperator, currentSymbol);
                        }

                        operatorsCollection.Push(currentSymbol);
                    }
                }
                else if (isParenthesis)
                {
                    if (currentSymbol == "(")
                    {
                        operatorsCollection.Push(currentSymbol);
                    }
                    else if (currentSymbol == ")")
                    {
                        string lastInsertedOperator = operatorsCollection.Peek();

                        while (lastInsertedOperator != "(" && operatorsCollection.Count != 0)
                        {
                            reversePolishNotation.Enqueue(lastInsertedOperator);
                            operatorsCollection.Pop();
                            if (operatorsCollection.Count == 0)
                            {
                                break;
                            }

                            lastInsertedOperator = operatorsCollection.Peek();
                        }

                        if (lastInsertedOperator == "(")
                        {
                            operatorsCollection.Pop();
                        }
                        else
                        {
                            throw new ArgumentException("No left parenthesis matched");
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            foreach (var op in operatorsCollection)
            {
                reversePolishNotation.Enqueue(op);
            }

            return reversePolishNotation;
        }

        private static decimal ParseNotation(Queue<string> notation)
        {
            throw new NotImplementedException();
        }

        private static bool IsNumeric(string value)
        {
            double currentNumber;
            bool isNumeric = double.TryParse(value, out currentNumber);

            return isNumeric;
        }

        private static bool IsParenthesis(string value)
        {
            bool isParenthesis = false;

            if (value == "(" || value == ")")
            {
                isParenthesis = true;
            }

            return isParenthesis;
        }

        private static bool IsOperator(string value)
        {
            bool isOperator = false;

            foreach (var supportedOperator in supportedOperations)
            {
                if (value == Convert.ToString(supportedOperator.Symbol))
                {
                    isOperator = true;
                    break;
                }
            }

            return isOperator;
        }

        private static bool ShouldAddOperatorToNotation(string lastInsertedOperatorString, string currentOperatorString)
        {
            if (IsParenthesis(lastInsertedOperatorString))
            {
                return false;
            }

            Operator lastInsertedOperator = supportedOperations.
                                                FirstOrDefault(x => Convert.ToString(x.Symbol) == lastInsertedOperatorString);
            Operator currentOperator = supportedOperations
                                                .FirstOrDefault(x => Convert.ToString(x.Symbol) == currentOperatorString);

            bool ShouldAddOperatorToNotation = false;

            if (currentOperator.Assosiativity == Assosiativity.Left &&
                (int)currentOperator.Precedence <= (int)lastInsertedOperator.Precedence)
            {
                ShouldAddOperatorToNotation = true;
            }

            if (currentOperator.Assosiativity == Assosiativity.Right &&
                (int)currentOperator.Precedence < (int)lastInsertedOperator.Precedence)
            {
                ShouldAddOperatorToNotation = true;
            }

            return ShouldAddOperatorToNotation;
        }
    }
}