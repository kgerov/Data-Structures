﻿namespace _05.TreeAndGraphTraversal
{
    using System.Collections.Generic;

    public class Graph
    {
        private List<int>[] graph;
        private bool[] isVisited;

        public Graph(int numberOfNodes)
        {
            this.graph = new List<int>[numberOfNodes];
            for (int i = 0; i < this.graph.Length; i++)
            {
                this.graph[i] = new List<int>();
            }

            this.isVisited = new bool[numberOfNodes];
        }

        public void AddChild(int parentNode, int childNode)
        {
            this.graph[parentNode].Add(childNode);
        }

        public int GetNumberOfRootNodes()
        {
            HashSet<int> childNodes = new HashSet<int>();

            foreach (List<int> node in this.graph)
            {
                foreach (int child in node)
                {
                    childNodes.Add(child);
                }
            }

            int parentCount = 0;

            for (int node = 0; node < this.graph.Length; node++)
            {
                if (!childNodes.Contains(node))
                {
                    parentCount++;
                }
            }

            return parentCount;
        }
    }
}
