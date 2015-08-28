namespace _04.LongestPathInATree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class FindMaxSumPath
    {
        private static Dictionary<int, List<int>> nodes = new Dictionary<int, List<int>>();
        private static Dictionary<int, int?> parents = new Dictionary<int, int?>();

        static void Main(string[] args)
        {
            int numberOfNodes = Int32.Parse(Console.ReadLine());
            int numberOfEdges = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfEdges; i++)
            {
                string inputLine = Console.ReadLine();
                string[] tokens = inputLine.Split(' ');

                int parent = Int32.Parse(tokens[0]); // First token is always parent (From task)
                int child = Int32.Parse(tokens[1]); // Second token is always child (From task)

                if (!nodes.ContainsKey(parent))
                {
                    nodes[parent] = new List<int>();
                }

                nodes[parent].Add(child);

                if (!parents.ContainsKey(child))
                {
                    parents[child] = parent;
                }
            }

            AddRootNodeToParentList();

            List<int> maxSumPath = FindMaxSumPathInTree();
            Console.WriteLine("The max sum path is: " + String.Join(" -> ", maxSumPath));
            Console.WriteLine("Sum: {0}", maxSumPath.Sum());
        }

        private static void AddRootNodeToParentList()
        {
            foreach (var node in nodes)
            {
                if (!parents.ContainsKey(node.Key))
                {
                    parents[node.Key] = null;
                    break;
                }
            }
        }

        private static int GetRootNode()
        {
            int root = parents.FirstOrDefault(x => x.Value == null).Key;

            return root;
        }

        private static List<int> FindMaxSumPathInTree()
        {
            List<int> leafNodes = GetLeafNodes();
            List<List<int>> pathsFromLeafNodes = GetPathsFromLeafNodes(leafNodes);
            List<List<int>> maxPathsFromChildsOfRoot = GetMaxPathsFromChildsOfRoot(pathsFromLeafNodes);

            List<int> maxSumPath = new List<int>();
            int maxSum = 0;

            for (int i = 0; i < maxPathsFromChildsOfRoot.Count; i++)
            {
                for (int j = i + 1; j < maxPathsFromChildsOfRoot.Count; j++)
                {
                    List<int> currentPath = ConcatPaths(maxPathsFromChildsOfRoot[i], maxPathsFromChildsOfRoot[j]);
                    
                    int currentPathSum = currentPath.Sum();

                    if (currentPathSum > maxSum)
                    {
                        maxSum = currentPathSum;
                        maxSumPath = currentPath;
                    }
                }
            }

            return maxSumPath;
        }

        private static List<int> GetLeafNodes()
        {
            List<int> leafNodes = new List<int>();

            foreach (var node in parents)
            {
                if (!nodes.ContainsKey(node.Key)) // Check if node has no children
                {
                    leafNodes.Add(node.Key);
                }
            }

            return leafNodes;
        }

        private static List<List<int>> GetPathsFromLeafNodes(List<int> leafNodes)
        {
            List<List<int>> pathsFromLeafNodes = new List<List<int>>();

            foreach (var node in leafNodes)
            {
                int currentNode = node;
                List<int> currentSum = new List<int>();
                currentSum.Add(currentNode);

                while (parents[currentNode] != null)
                {
                    currentNode = Convert.ToInt32(parents[currentNode]);
                    currentSum.Add(currentNode);
                }

                pathsFromLeafNodes.Add(currentSum);
            }

            return pathsFromLeafNodes;
        }

        private static List<List<int>> GetMaxPathsFromChildsOfRoot(List<List<int>> maxPaths)
        {
            List<List<int>> maxPathsFromChildsOfRoot = new List<List<int>>();

            int root = GetRootNode();

            foreach (var rootChild in nodes[root])
            {
                IEnumerable<List<int>> pathsThroughChild = maxPaths.Where(x => x.Contains(rootChild));
                List<int> maxPathFromCurrenChild = new List<int>();
                bool isFirstChild = true;

                foreach (var path in pathsThroughChild)
                {
                    if (isFirstChild || path.Sum() >= maxPathFromCurrenChild.Sum())
                    {
                        maxPathFromCurrenChild = path;
                        isFirstChild = false;
                    }
                }

                maxPathsFromChildsOfRoot.Add(maxPathFromCurrenChild);
            }

            return maxPathsFromChildsOfRoot;
        }

        private static List<int> ConcatPaths(List<int> firstList, List<int> secondList)
        {
            List<int> newList = CloneList(firstList);
            List<int> secondListWithoutRoot = CloneList(secondList);
            int root = GetRootNode();

            secondListWithoutRoot.Remove(root);

            for (int i = secondListWithoutRoot.Count - 1; i >= 0; i--)
            {
                newList.Add(secondListWithoutRoot[i]);
            }

            return newList;
        }

        private static List<int> CloneList(List<int> originalList)
        {
            List<int> clonedList = new List<int>();

            foreach (var item in originalList)
            {
                clonedList.Add(item);
            }

            return clonedList;
        }
    }
}