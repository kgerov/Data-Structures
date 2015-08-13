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
                new Operator('+', Precedence.Low, Assosiativity.Left, 2),
                new Operator('-', Precedence.Low, Assosiativity.Left, 2),
                new Operator('*', Precedence.Medium, Assosiativity.Left, 2),
                new Operator('/', Precedence.Medium, Assosiativity.Left, 2),
                new Operator('^', Precedence.High, Assosiativity.Right, 2),
            };
        }

        public static decimal CalculateExpression(string expression)
        {
            Queue<string> notation = ConvertExpressionToNotation(expression);
            decimal expressionValue = ParseNotation(notation);

            return expressionValue;
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
            Stack<decimal> numbersToEvaluate = new Stack<decimal>();

            foreach (var expressionToken in notation)
            {
                if (IsNumeric(expressionToken))
                {
                    numbersToEvaluate.Push(Convert.ToDecimal(expressionToken));
                }
                else if (IsOperator(expressionToken))
                {
                    Stack<decimal> arguments = new Stack<decimal>();
                    Operator currentOperator = supportedOperations.
                                                FirstOrDefault(x => Convert.ToString(x.Symbol) == expressionToken);

                    if (currentOperator.ArgumentsTaken > numbersToEvaluate.Count)
                    {
                        throw new ArgumentException("Invalid expression.");
                    }

                    for (int i = 0; i < currentOperator.ArgumentsTaken; i++)
                    {
                        decimal currentValue = numbersToEvaluate.Pop();
                        arguments.Push(currentValue);
                    }

                    decimal result = ExecuteOperation(arguments, currentOperator);
                    numbersToEvaluate.Push(result);
                }
            }

            if (numbersToEvaluate.Count != 1)
            {
                throw new ArgumentException("Invalid expression");
            }

            decimal finalResult = numbersToEvaluate.Pop();
            return finalResult;
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

        private static decimal ExecuteOperation(Stack<decimal> arguments, Operator expresssionOperator)
        {
            decimal result = arguments.Pop();

            switch (expresssionOperator.Symbol)
            {
                case '+': result += arguments.Pop();
                    break;
                case '-': result -= arguments.Pop();
                    break;
                case '*': result *= arguments.Pop();
                    break;
                case '/': result /= arguments.Pop();
                    break;
                case '^':
                    result = Convert.ToDecimal(Math.Pow(Convert.ToDouble(result), Convert.ToDouble(arguments.Pop())));
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}