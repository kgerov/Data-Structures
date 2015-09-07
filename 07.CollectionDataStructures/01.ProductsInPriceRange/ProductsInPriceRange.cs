using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Wintellect.PowerCollections;

namespace _01.ProductsInPriceRange
{
    class ProductsInPriceRange
    {
        static void Main()
        {
            OrderedMultiDictionary<double, string> products = new OrderedMultiDictionary<double, string>(false);

            // Test 1: Input from console

            int numberOfProducts = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfProducts; i++)
            {
                string inputLine = Console.ReadLine();
                string[] tokens = inputLine.Split(' ');
                string productName = tokens[0];
                double price = Double.Parse(tokens[1]);

                products.Add(price, productName);
            }

            string priceRange = Console.ReadLine();
            string[] priceRangeTokens = priceRange.Split(' ');
            double minPrice = Double.Parse(priceRangeTokens[0]);
            double maxPrice = Double.Parse(priceRangeTokens[1]);

            var productsWithinRange = products.Range(minPrice, true, maxPrice, true);

            foreach (var product in productsWithinRange)
            {
                foreach (var productName in product.Value)
                {
                    Console.WriteLine("{0} {1}", product.Key, productName);
                }
            }

            // Test 2: Performance
            products = new OrderedMultiDictionary<double, string>(false);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 500000; i++)
            {
                products.Add(i, "product" + i);
            }

            Random rnd = new Random();

            for (int i = 0; i < 10000; i++)
            {
                double min = rnd.NextDouble() * 500000;
                double max = rnd.NextDouble() * 500000;

                var range = products.Range(min, true, max, true);
            }

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
