using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.RemoveOddOccurences
{
    class RemoveOddOccurences
    {
        static void Main()
        {
            Console.WriteLine("Enter integers seperated by a space: ");            
            string input = Console.ReadLine();

            Dictionary<int, int> repetitions = new Dictionary<int, int>();
            List<int> nums = input.Split(' ').ToList().ConvertAll(x => Convert.ToInt32(x));

            foreach (var num in nums)
            {
                if (repetitions.ContainsKey(num))
                {
                    repetitions[num]++;
                }
                else
                {
                    repetitions[num] = 1;
                }
            }

            foreach (int key in repetitions.Keys)
            {
                if (repetitions[key] % 2 != 0)
                {
                    nums.RemoveAll(x => x == key);
                }
            }

            Console.WriteLine(String.Join(" ", nums));

            Main();
        }
    }
}
