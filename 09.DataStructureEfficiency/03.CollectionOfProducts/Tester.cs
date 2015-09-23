using System;
using System.Collections.Generic;
using _03.CollectionOfProducts.DataStructure;

namespace _03.CollectionOfProducts
{
    class Tester
    {
        static void Main()
        {
            Dictionary<int, string> test = new Dictionary<int, string>();

            test.Add(1, "asdf");

            string ram;

            test.TryGetValue(2, out ram);
            Console.WriteLine(ram == null);
        }
    }
}
