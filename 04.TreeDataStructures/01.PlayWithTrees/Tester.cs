using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Tester
    {
        static void Main()
        {
            bool isTreeInput = true;
            bool isRoot = true;
            int? p = null;
            Tree<int> root = null;

            while(isTreeInput)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(' ');
                IList<int> nums = new List<int>();
                
                foreach (var token in tokens)
                {
                    nums.Add(Int32.Parse(token));
                }

                if (nums.Count == 1)
                {
                    p = nums[0];
                    isTreeInput = false;
                }
                else
                {
                    if (isRoot)
                    {
                        root = new Tree<int>(nums[0]);
                        isRoot = false;
                    }

                    Tree<int> parent = GetNodeByValue(nums[0], root);
                    parent.AddChild(new Tree<int>(nums[1]));
                }
            }

            int s = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Root node: {0}", root.Value);

            List<int> leafNodes = new List<int>();
            FindLeafNodes(root, leafNodes);
            leafNodes.Sort();
            Console.WriteLine("Leaf nodes: {0}", String.Join(", ", leafNodes));

            List<int> middleNodes = new List<int>();
            FindMiddleNodes(root, middleNodes);
            middleNodes.Sort();
            Console.WriteLine("Middle nodes: {0}", String.Join(", ", middleNodes));

            List<int> longestPath = new List<int>();
            FindLongestPath(new List<int>() { root.Value }, ref longestPath, root);
            Console.WriteLine("LongestPath: {0} (length = {1})", String.Join(", ", longestPath), longestPath.Count);

            List<List<int>> pathsWithGivenSum = new List<List<int>>();
            FindPathWithGivenSum(new List<int>() { root.Value }, ref pathsWithGivenSum, root, p);
            Console.WriteLine("Paths with sum {0}", p);
            pathsWithGivenSum.ForEach(x => Console.WriteLine(String.Join(" -> ", x)));

            List<List<int>> subtrees = new List<List<int>>();
            FindSubtrees(root, ref subtrees);
            Console.WriteLine("Subtrees with sum {0}:", s);
            foreach (var subtree in subtrees)
            {
                if (subtree.Sum() == s)
                {
                    Console.WriteLine(String.Join(" + ", subtree));
                }
            }

            Console.WriteLine("The End");
            Main();
        }

        public static Tree<int> GetNodeByValue(int value, Tree<int> node)
        {
            if (node.Value == value)
            {
                return node;
            }
            else
            {
                foreach (var item in node.Children)
                {
                    Tree<int> possible = GetNodeByValue(value, item);

                    if (possible.Parent != null)
                    {
                        return possible;
                    }
                }
            }

            return new Tree<int>(value);
        }

        public static void FindLeafNodes(Tree<int> node, IList<int> leafNodes)
        {
            if (node.Children.Count == 0)
            {
                leafNodes.Add(node.Value);
            }
            else
            {
                foreach (var item in node.Children)
                {
                    FindLeafNodes(item, leafNodes);
                }
            }
        }

        public static void FindMiddleNodes(Tree<int> node, IList<int> middleNodes)
        {
            if (node.Children.Count != 0 && node.Parent != null)
            {
                middleNodes.Add(node.Value);
            }
            
            foreach (var item in node.Children)
            {
                FindMiddleNodes(item, middleNodes);
            }
        }

        public static void FindLongestPath(List<int> currentPath, ref List<int> longestPath, Tree<int> currentNode)
        {
            foreach (var child in currentNode.Children)
            {
                currentPath.Add(child.Value);

                if (child.Children.Count == 0 && currentPath.Count > longestPath.Count)
                {
                    longestPath = new List<int>(currentPath);
                }
                else
                {
                    FindLongestPath(currentPath, ref longestPath, child);
                }

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        public static void FindPathWithGivenSum(List<int> currentPath, ref List<List<int>> longestPaths, Tree<int> currentNode, int? sum)
        {
            foreach (var child in currentNode.Children)
            {
                currentPath.Add(child.Value);

                if (child.Children.Count == 0 && currentPath.Sum(x => x) == sum)
                {
                    longestPaths.Add(new List<int>(currentPath));
                }
                else
                {
                    FindPathWithGivenSum(currentPath, ref longestPaths, child, sum);
                }

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        public static void FindSubtrees(Tree<int> currentNode, ref List<List<int>> subtrees)
        {
            foreach (var child in currentNode.Children)
            {
                if (child.Children.Count > 0)
                {
                    List<int> subtree = child.Children.Select(x => x.Value).ToList();
                    subtree.Add(child.Value);
                    
                    for(int i = 0; i < subtrees.Count; i++)
                    {
                        bool isInSub = false;
                        
                        foreach (var item in subtrees[i])
                        {
                            if (item == child.Value)
                            {
                                isInSub = true;
                            }
                        }

                        if (isInSub)
                        {
                            subtrees[i] = subtrees[i].Concat(subtree.Where(x => x != child.Value).ToList()).ToList();
                        }
                    }

                    subtrees.Add(subtree);
                }

                FindSubtrees(child, ref subtrees);
            }
        }
    }
}