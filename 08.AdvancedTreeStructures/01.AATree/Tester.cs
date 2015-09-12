using System;
using _01.AATree.DataStructure;

namespace _01.AATree
{
    class Tester
    {
        public static void Main()
        {
            var tree = new AATree<int>();
            Console.WriteLine("The AA tree created.");
            var nums = new[] { -5, 20, 14, 11, 8, -3, 111, 7, 100, -55 };
            for (int i = 0; i < nums.Length; i++)
            {
                AddNumber(tree, nums[i]);
            }

            Console.WriteLine(tree.Contains(-5));
            Console.WriteLine(tree.Contains(8));
            Console.WriteLine(tree.Contains(1000));
        }

        public static void AddNumber(AATree<int> tree, int key)
        {
            tree.Add(key);
            Console.WriteLine("Added " + key);

            DisplayTree(tree.Root, string.Empty);
            Console.WriteLine("----------------------");
        }

        private static void DisplayTree(AANode<int> node, string intend)
        {
            Console.WriteLine(intend + node.Value + " (level:" + node.Level + ")");
            if (node.LeftChild.Level != 0)
            {
                DisplayTree(node.LeftChild, intend + "  ");
            }
            if (node.RightChild.Level != 0)
            {
                DisplayTree(node.RightChild, intend + "  ");
            }
        }
    }
}
