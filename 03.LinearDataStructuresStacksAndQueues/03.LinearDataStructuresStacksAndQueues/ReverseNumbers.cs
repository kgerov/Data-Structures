using System;
using System.Collections.Generic;

namespace _03.LinearDataStructuresStacksAndQueues
{
    class ReverseNumbers
    {
        static void Main()
        {
            Stack<int> nums = new Stack<int>();
            Console.Write("Enter integers seperated by a space: ");
            string line = Console.ReadLine();

            if (!String.IsNullOrEmpty(line))
            {
                string[] stringNums = line.Split(' ');

                foreach (var stringNum in stringNums)
                {
                    nums.Push(Convert.ToInt32(stringNum));
                }

                while (nums.Count > 0)
                {
                    Console.Write(nums.Pop() + " ");
                }
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine();
            Main();
        }
    }
}
