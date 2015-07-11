using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LongestSubsequence
{
    class FindLongestSubsequence
    {
        static void Main()
        {
            Console.WriteLine("Enter words seperated by a space: ");
            string input = Console.ReadLine();

            List<string> nums = input.Split(' ').ToList();
            string longestNum = nums[0];
            int longestLength = 1;
            string currentNum = nums[0];
            int currentLength = 1;


            for (int i = 0; i < nums.Count-1; i++)
            {
                if (currentNum == nums[i+1])
                {
                    currentLength++;
                }
                else
                {
                    currentLength = 1;
                    currentNum = nums[i+1];
                }

                if (currentLength > longestLength)
                {
                    longestLength = currentLength;
                    longestNum = currentNum;
                }
            }

            for (int i = 0; i < longestLength; i++)
            {
                Console.Write(longestNum + " ");
            }

            Console.WriteLine();

            Main();
        }
    }
}
