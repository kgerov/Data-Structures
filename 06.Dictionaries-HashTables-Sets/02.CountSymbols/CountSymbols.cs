using System;
using System.Linq;
using DictionaryDataStructure;

namespace _02.CountSymbols
{
    class CountSymbols
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> symbols = new Dictionary<char, int>();
   
            string text = Console.ReadLine();

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = Convert.ToChar(text[i]);

                if (!symbols.ContainsKey(text[i]))
                {
                    symbols[currentChar] = 0;
                }

                symbols[currentChar]++;
            }

            var orderedSymbols = symbols.ToList();
            orderedSymbols.Sort(delegate(KeyValue<char, int> c1, KeyValue<char, int> c2) { return c1.Key.CompareTo(c2.Key); });

            foreach (var symbol in orderedSymbols)
            {
                Console.WriteLine(symbol + " time/s");
            }
        }
    }
}
