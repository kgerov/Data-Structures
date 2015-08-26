namespace _02.RoundDance
{
    using System;
    using System.Collections.Generic;

    class RoundDance
    {
        private static Dictionary<int, List<int>> people = new Dictionary<int, List<int>>();
        private static HashSet<int> isVisited;

        static void Main()
        {
            int numberOfFriendships = Int32.Parse(Console.ReadLine());
            int manWhoLeads = Int32.Parse(Console.ReadLine());

            isVisited = new HashSet<int>();

            for (int i = 0; i < numberOfFriendships; i++)
            {
                string inputLine = Console.ReadLine();
                string[] inputTokens = inputLine.Split(' ');

                int firstFriendValue = Int32.Parse(inputTokens[0]);
                int secondFriendValue = Int32.Parse(inputTokens[1]);

                if (!people.ContainsKey(firstFriendValue))
                {
                    people[firstFriendValue] = new List<int>();
                }

                people[firstFriendValue].Add(secondFriendValue);

                if (!people.ContainsKey(secondFriendValue))
                {
                    people[secondFriendValue] = new List<int>();
                }

                people[secondFriendValue].Add(firstFriendValue);
            }


            int longestPath = FindLongestPath(manWhoLeads);

            Console.WriteLine(longestPath);
        }

        public static int FindLongestPath(int leadingNode)
        {
            int currentPath = 1; // Takes into account the leading node
            int longestPath = 1;

            DFS(leadingNode, ref currentPath, ref longestPath);

            return longestPath;

        }

        private static void DFS(int node, ref int currentPathLength, ref int longestPathLength)
        {
            if (!isVisited.Contains(node))
            {
                isVisited.Add(node);

                foreach (var child in people[node])
                {
                    currentPathLength++;

                    if (!HasUnvisitedChilds(child))
                    {
                        longestPathLength = currentPathLength;
                    }
                    else
                    {
                        DFS(child, ref currentPathLength, ref longestPathLength);
                    }

                    currentPathLength--;
                }
            }
        }

        public static bool HasUnvisitedChilds(int node)
        {
            bool hasUnvisitedChilds = false;

            foreach (var child in people[node])
            {
                if (!isVisited.Contains(child))
                {
                    hasUnvisitedChilds = true;
                    break;
                }
            }

            return hasUnvisitedChilds;
        }
    }
}
