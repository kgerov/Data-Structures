using System;
using _04.OrderedSet.DataStructures;

namespace _04.OrderedSet
{
    class Tester
    {
        static void Main(string[] args)
        {
            OrderedSet<int> set = new OrderedSet<int>();

            set.Add(17);
            set.Add(9);
            set.Add(12);
            set.Add(19);
            set.Add(6);
            set.Add(25);

            Console.WriteLine(set.Contains(105));

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            set.Remove(25);
            set.Remove(9);
            Console.WriteLine("\n");
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            set = new OrderedSet<int>();

            set.Add(1);
            set.Add(2);
            set.Add(3);
            set.Add(4);
            set.Add(5);
            set.Add(6);
            set.Add(7);

            Console.WriteLine();
        }
    }
}
