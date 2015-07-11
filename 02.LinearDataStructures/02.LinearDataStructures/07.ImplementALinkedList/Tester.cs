using System;
using System.Runtime.CompilerServices;

namespace _07.ImplementALinkedList
{
    class Tester
    {
        static void Main()
        {
            SingleLinkedList<int> test = new SingleLinkedList<int>();

            test.Add(5);
            test.Add(2);
            test.Add(10);
            test.Remove(0);
            test.Add(10);

            Console.WriteLine(test.LastIndexOf(10));

            Console.WriteLine("--" + test.Count);

            foreach (var i in test)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}
