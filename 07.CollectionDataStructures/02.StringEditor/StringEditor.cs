using System;
using System.Diagnostics;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.StringEditor
{
    class StringEditor
    {
        static void Main()
        {
            BigList<char> text = new BigList<char>();

            // Test 1: Console Input

            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] tokens = input.Split(' ');

                switch (tokens[0])
                {
                    case "APPEND": Append(text, String.Join(" ", tokens.Where(x => x != "APPEND")));
                        break;
                    case "INSERT": Insert(text, Int32.Parse(tokens[1]), tokens[2]);
                        break;
                    case "DELETE": Delete(text, Int32.Parse(tokens[1]), Int32.Parse(tokens[2]));
                        break;
                    case "PRINT": Print(text);
                        break;
                    case "REPLACE": Replace(text, Int32.Parse(tokens[1]), Int32.Parse(tokens[2]), tokens[3]);
                        break;
                }

                input = Console.ReadLine();
            }

            // Test 2: Performance

            text = new BigList<char>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                Append(text, "pesho", false);
                Append(text, "123", false);
                Insert(text, 0, "456", false);
                Delete(text, 1, 2, false);
                Delete(text, 0, 5 + i, false);
                Append(text, "pepi", false);
                Replace(text, 1, 5, "kiro", false);
                Replace(text, 0, 5 + i, "krasi", false);
                Delete(text, 0, 2, false);
                Append(text, "Hello C#", false);
            }

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.Elapsed);
        }

        private static void Append(BigList<char> text, string textToAppend, bool shouldPrint = true)
        {
            for (int i = 0; i < textToAppend.Length; i++)
            {
                text.Add(textToAppend[i]);
            }

            PrintSuccessMessage(shouldPrint);
        }

        private static void Insert(BigList<char> text, int position, string textToAppend, bool shouldPrint = true)
        {
            if (position < 0 || position >= text.Count)
            {
                PrintErrorMessage(shouldPrint);
                return;
            }


            for (int i = 0; i < textToAppend.Length; i++)
            {
                text.Insert(position + i, textToAppend[i]);
            }

            PrintSuccessMessage(shouldPrint);
        }

        private static void Delete(BigList<char> text, int position, int count, bool shouldPrint = true)
        {
            if (position < 0 || position + count >= text.Count)
            {
                PrintErrorMessage(shouldPrint);
                return;
            }

            text.RemoveRange(position, count);

            PrintSuccessMessage(shouldPrint);
        }

        private static void Replace(BigList<char> text, int position, int count, string replacementText, bool shouldPrint = true)
        {
            if (position < 0 || position + count >= text.Count)
            {
                PrintErrorMessage(shouldPrint);
                return;
            }

            Delete(text, position, count, false);
            Insert(text, position, replacementText, false);

            PrintSuccessMessage(shouldPrint);
        }

        private static void Print(BigList<char> text)
        {
            text.ForEach(Console.Write);
            Console.WriteLine();
        }

        private static void PrintSuccessMessage(bool shouldPrint = true)
        {
            if (shouldPrint)
            {
                Console.WriteLine("Ok");
            }
        }

        private static void PrintErrorMessage(bool shouldPrint = true)
        {
            if (shouldPrint)
            {
                Console.WriteLine("Error");
            }
        }
    }
}