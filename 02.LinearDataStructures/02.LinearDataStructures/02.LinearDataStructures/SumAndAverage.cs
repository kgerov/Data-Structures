using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.LinearDataStructures
{
    class SumAndAverage
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter integers seperated by a space: ");
                string input = Console.ReadLine();

                List<int> nums = new List<int>();
                int sum = 0;
                double average = 0;

                try
                {
                    nums = input.Split(' ').ToList().ConvertAll(x => Convert.ToInt32(x));
                    sum = nums.Sum();
                    average = sum*1.0/nums.Count();
                }
                catch (FormatException)
                {
                    
                }

                Console.WriteLine("The sum is: " + sum);
                Console.WriteLine("The average is: " + average);
            }
        }
    }
}
