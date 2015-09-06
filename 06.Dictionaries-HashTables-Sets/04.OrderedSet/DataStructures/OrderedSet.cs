namespace _04.OrderedSet.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        public OrderedSet()
        {
            this.Count = 0;
        }

        public Node<T> RootNode { get; set; }

        public int Count { get; set; }

        public void Add(T element)
        {
            this.AddWithoutBalance(element);
            this.Balance();
        }

        public void AddWithoutBalance(T element)
        {
            Node<T> newElement = new Node<T>(element);
            Node<T> currentNode = this.RootNode;
            bool nodeIsFound = false;

            if (this.Count == 0)
            {
                this.RootNode = newElement;
                nodeIsFound = true;
            }

            while (!nodeIsFound)
            {
                int nodeCompare = currentNode.CompareTo(newElement);

                nodeIsFound = (nodeCompare > 0 && currentNode.SmallerChild == null) ||
                              (nodeCompare < 0 && currentNode.BiggerChild == null);

                if (nodeCompare > 0 && !nodeIsFound)
                {
                    currentNode = currentNode.SmallerChild;
                }
                else if (nodeCompare < 0 && !nodeIsFound)
                {
                    currentNode = currentNode.BiggerChild;
                }
                else if (nodeCompare == 0)
                {
                    throw new ArgumentException(String.Format("Element {0} already exists in set", element));
                }
            }

            if (currentNode != null && currentNode.CompareTo(newElement) > 0)
            {
                currentNode.SmallerChild = newElement;
            }
            else if (currentNode != null && currentNode.CompareTo(newElement) < 0)
            {
                currentNode.BiggerChild = newElement;
            }

            newElement.Parent = currentNode;
            this.Count++;
        }

        public bool Contains(T element)
        {
            Node<T> node = FindElement(element);

            if (node == null)
            {
                return false;
            }

            return true;
        }

        public bool Remove(T element)
        {
            Node<T> nodeToRemove = FindElement(element);

            return this.Remove(nodeToRemove);
        }

        public bool Remove(Node<T> nodeToRemove)
        {
            // case with removal of root 
            bool shouldDecreaseCount = true;

            if (nodeToRemove == null) // No such element exists
            {
                return false;
            }

            Node<T> nodeToRemoveParent = nodeToRemove.Parent;

            if (!nodeToRemove.HasChildren()) // node has no children
            {
                if (nodeToRemoveParent.SmallerChild != null && nodeToRemoveParent.SmallerChild.CompareTo(nodeToRemove) == 0)
                {
                    nodeToRemoveParent.SmallerChild = null;
                }
                else
                {
                    nodeToRemoveParent.BiggerChild = null;
                }
            }
            else if (nodeToRemove.SmallerChild == null && nodeToRemove.BiggerChild != null) // node has 1 child, and that child is the right one
            {
                if (nodeToRemoveParent.SmallerChild != null && nodeToRemoveParent.SmallerChild.CompareTo(nodeToRemove) == 0)
                {
                    nodeToRemoveParent.SmallerChild = nodeToRemove.BiggerChild;
                }
                else
                {
                    nodeToRemoveParent.BiggerChild = nodeToRemove.BiggerChild;
                }
            }
            else if (nodeToRemove.SmallerChild != null && nodeToRemove.BiggerChild == null) // node has 1 child, and that child is the left one
            {
                if (nodeToRemoveParent.SmallerChild != null && nodeToRemoveParent.SmallerChild.CompareTo(nodeToRemove) == 0)
                {
                    nodeToRemoveParent.SmallerChild = nodeToRemove.SmallerChild;
                }
                else
                {
                    nodeToRemoveParent.BiggerChild = nodeToRemove.SmallerChild;
                }
            }
            else // node has 2 child nodes
            {
                Node<T> minimumNode = FindMinimumNodeInSubtree(nodeToRemove.BiggerChild);
                nodeToRemove.Value = minimumNode.Value;
                this.Remove(minimumNode);

                shouldDecreaseCount = false;
            }

            if (shouldDecreaseCount)
            {
                this.Count--;
            }

            return true;
        }

        public void Balance()
        {
            List<T> orderedSet = new List<T>();

            foreach (var element in this)
            {
                orderedSet.Add(element);
            }

            List<T> firstHalf = orderedSet.Take((orderedSet.Count + 1) / 2).ToList();
            List<T> secondHalf = orderedSet.Skip((orderedSet.Count + 1) / 2).ToList();

            T midElement = firstHalf[firstHalf.Count - 1];
            firstHalf.RemoveAt(firstHalf.Count - 1);

            this.RootNode = null;
            this.Count = 0;
            this.AddWithoutBalance(midElement);

            this.BalanceHelper(firstHalf);
            this.BalanceHelper(secondHalf);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Stack<T> nodesToTraverse = new Stack<T>();
            Node<T> currentNode = this.RootNode;

            while (nodesToTraverse.Count > 0 || currentNode != null)
            {
                if (currentNode != null)
                {
                    nodesToTraverse.Push(currentNode.Value);
                    currentNode = currentNode.SmallerChild;
                }
                else
                {
                    Node<T> nextSmallestNode = FindElement(nodesToTraverse.Pop());
                    yield return nextSmallestNode.Value;
                    currentNode = nextSmallestNode.BiggerChild;
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Node<T> FindElement(T element)
        {
            if (this.Count == 0)
            {
                return null;
            }

            Node<T> newElement = new Node<T>(element);
            Node<T> currentNode = this.RootNode;

            while (currentNode != null && currentNode.CompareTo(newElement) != 0)
            {
                int nodeCompare = currentNode.CompareTo(newElement);

                if (nodeCompare > 0)
                {
                    currentNode = currentNode.SmallerChild;
                }
                else if (nodeCompare < 0)
                {
                    currentNode = currentNode.BiggerChild;
                }
            }
            
            return currentNode;
        }

        private Node<T> FindMinimumNodeInSubtree(Node<T> node)
        {
            Node<T> smallestNode = node;

            while (node != null)
            {
                node = node.SmallerChild;

                if (node != null && smallestNode.CompareTo(node) > 0)
                {
                    smallestNode = node;
                }
            }

            return smallestNode;
        }

        private bool BalanceHelper(List<T> elementsToAdd)
        {
            if (elementsToAdd.Count == 0)
            {
                return false;
            }

            List<T> firstHalf = elementsToAdd.Take((elementsToAdd.Count + 1) / 2).ToList();
            List<T> secondHalf = elementsToAdd.Skip((elementsToAdd.Count + 1) / 2).ToList();

            T midElement = firstHalf[firstHalf.Count - 1];
            firstHalf.RemoveAt(firstHalf.Count - 1);

            Node<T> element = new Node<T>(midElement);
            this.AddWithoutBalance(element.Value);

            this.BalanceHelper(firstHalf);

            if (secondHalf.Count > 0)
            {
                this.BalanceHelper(secondHalf);
            }

            return true;
        }
    }
}