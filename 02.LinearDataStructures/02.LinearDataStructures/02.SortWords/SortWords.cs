using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.SortWords
{
    class SortWords
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter words seperated by a space: ");
                string input = Console.ReadLine();

                List<string> words = input.Split(' ').ToList();

                words.Sort();

                Console.WriteLine(String.Join(" ", words));
            }
        }
    }
}
