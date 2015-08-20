using System;

namespace _05.TreeAndGraphTraversal
{
    class Tester
    {
        static void Main()
        {
            int numberOfNodes = Int32.Parse(Console.ReadLine());
            int numberOfEdges = Int32.Parse(Console.ReadLine());

            Graph graph = new Graph(numberOfNodes);

            for (int i = 0; i < numberOfEdges; i++)
            {
                string lineWithNodes = Console.ReadLine();
                string[] stringNodes = lineWithNodes.Split(' ');

                int currentParentNode = Int32.Parse(stringNodes[0]); // First token is awlays the parent
                int currentChildNode = Int32.Parse(stringNodes[1]); // Second token is awlays the child
                
                graph.AddChild(currentParentNode, currentChildNode);
            }

            int parentCount = graph.GetNumberOfRootNodes();

            switch (parentCount)
            {
                case 0:
                    Console.WriteLine("No root nodes");
                    break;
                case 1:
                    Console.WriteLine("One root");
                    break;
                default:
                    Console.WriteLine("Multiple roots");
                    break;
            }

            Main();
        }
    }
}
