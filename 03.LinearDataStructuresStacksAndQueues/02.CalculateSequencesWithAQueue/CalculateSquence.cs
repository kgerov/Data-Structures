using System;
using System.Collections.Generic;

namespace _02.CalculateSequencesWithAQueue
{
    class CalculateSquence
    {
        static void Main()
        {
            Console.Write("Enter starting integer: ");
            int n = Int32.Parse(Console.ReadLine());
            int counter = 1;

            Queue<int> sequence = new Queue<int>();
            sequence.Enqueue(n);

            while (counter <= 50)
            {
                Console.Write(sequence.Peek() + ", ");
                int currentNum = sequence.Dequeue();
                sequence.Enqueue(currentNum + 1);
                sequence.Enqueue(2*currentNum + 1);
                sequence.Enqueue(currentNum + 2);
                counter++;
            }

            Console.WriteLine("\n");
            Main();
        }
    }
}
