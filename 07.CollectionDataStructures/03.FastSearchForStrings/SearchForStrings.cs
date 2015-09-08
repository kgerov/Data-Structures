using System;
using System.Linq;
using System.Text;

namespace _03.FastSearchForStrings
{
    class SearchForStrings
    {
        static void Main()
        {
            int numberOfWords = Int32.Parse(Console.ReadLine());
            string[] words = new string[numberOfWords];

            for (int i = 0; i < numberOfWords; i++)
            {
                words[i] = Console.ReadLine();
            }

            StringBuilder buffer = new StringBuilder();
            string[] wordsToLower = words.Select(x => x.ToLower()).ToArray();
            int[] numberOfOccurences = new int[numberOfWords];
            int numberOfLines = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfLines; i++)
            {
                string line = Console.ReadLine().ToLower();

                for (int charPos = 0; charPos < line.Length; charPos++)
                {
                    char currentSymbol = Convert.ToChar(line[charPos]);
                    bool isSeperator = IsSeperator(currentSymbol);

                    if (!isSeperator)
                    {
                        buffer.Append(currentSymbol);

                        for(int j = 0; j < numberOfWords; j++)
                        {
                            if (EndsWithWord(buffer, wordsToLower[j]))
                            {
                                numberOfOccurences[j]++;
                            }
                        }
                    }
                    else
                    {
                        buffer.Clear();
                    }
                }
            }

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine("{0} -> {1}", words[i], numberOfOccurences[i]);
            }   
        }

        private static bool IsSeperator(char currentSymbol)
        {
            bool isSeperator = currentSymbol == ' ' || currentSymbol == '.' ||
                               currentSymbol == '?' || currentSymbol == '!' ||
                               currentSymbol == ',' || currentSymbol == '"';

            return isSeperator;
        }

        private static bool EndsWithWord(StringBuilder buffer, string word)
        {
            int bufferLength = buffer.Length;
            int wordLength = word.Length;

            if (wordLength > bufferLength)
            {
                return false;
            }

            for (int i = bufferLength - wordLength, wordPosition = 0; i < bufferLength; i++, wordPosition++)
            {
                if (buffer[i] != word[wordPosition])
                {
                    return false;
                }
            }

            return true;
        }
    }
}