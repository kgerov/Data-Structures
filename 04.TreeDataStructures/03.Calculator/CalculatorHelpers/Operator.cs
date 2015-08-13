namespace _03.Calculator.CalculatorHelpers
{
    public class Operator
    {
        public Operator(char symbol, Precedence precedence, Assosiativity assosiativity)
        {
            this.Symbol = symbol;
            this.Precedence = precedence;
            this.Assosiativity = assosiativity;
        }

        public char Symbol { get; set; }

        public Precedence Precedence { get; set; }

        public Assosiativity Assosiativity { get; set; }
    }
}
