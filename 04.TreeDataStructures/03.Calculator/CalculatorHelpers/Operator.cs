namespace _03.Calculator.CalculatorHelpers
{
    public class Operator
    {
        public Operator(char symbol, Precedence precedence, Assosiativity assosiativity, int argumentsTaken)
        {
            this.Symbol = symbol;
            this.Precedence = precedence;
            this.Assosiativity = assosiativity;
            this.ArgumentsTaken = argumentsTaken;
        }

        public char Symbol { get; set; }

        public Precedence Precedence { get; set; }

        public Assosiativity Assosiativity { get; set; }

        public int ArgumentsTaken { get; set; }
    }
}
