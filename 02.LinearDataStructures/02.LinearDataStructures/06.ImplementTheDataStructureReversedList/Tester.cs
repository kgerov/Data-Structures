using System;

namespace _06.ImplementTheDataStructureReversedList
{
    class Tester
    {
        static void Main(string[] args)
        {
            ReversedList<int> test = new ReversedList<int>();

            test.Add(4);
            test.Add(5);
            test.Add(1);
            test.Add(21);
            test.Remove(3);
            test.Add(10);
            test.Add(15);
            test[0] = 14;

            foreach (var num in test)
            {
                Console.WriteLine(num);
            }
        }
    }
}
