using System;
using System.Collections;
using System.Collections.Generic;

namespace _01.AATree.DataStructure
{
    public class AATree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private AANode<T> nullNode;
        private AANode<T> deleted;
        private AANode<T> root;
 
        public AATree()
        {
            this.Count = 0;
            this.Root = this.nullNode = new AANode<T>();
            this.deleted = null;
        }

        public int Count { get; set; }

        public AANode<T> Root 
        {
            get { return this.root; }
            set { this.root = value; }
        }

        public void Add(T value)
        {
            this.Add(ref this.root, value);
        }

        public bool Remove(T value)
        {
            return this.Remove(ref this.root, value);
        }

        public bool Contains(T value)
        {
            AANode<T> currentNode = this.Root;

            while (currentNode != nullNode)
            {
                int valueCompare = value.CompareTo(currentNode.Value);

                if (valueCompare > 0)
                {
                    currentNode = currentNode.RightChild;
                }
                else if (valueCompare < 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<AANode<T>> nodesForTraversal = new Queue<AANode<T>>();
            nodesForTraversal.Enqueue(this.Root);

            while (nodesForTraversal.Count > 0)
            {
                AANode<T> currentNode = nodesForTraversal.Dequeue();
                yield return currentNode.Value;

                if (currentNode.LeftChild != this.nullNode)
                {
                    nodesForTraversal.Enqueue(currentNode.LeftChild);
                }

                if (currentNode.RightChild != this.nullNode)
                {
                    nodesForTraversal.Enqueue(currentNode.RightChild);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Add(ref AANode<T> node, T value)
        {
            if (node == this.nullNode)
            {
                node = new AANode<T>(value, this.nullNode);
                return;
            }

            int valueCompare = value.CompareTo(node.Value);

            if (valueCompare > 0)
            {
                this.Add(ref node.rightChild, value);
            }
            else if (valueCompare < 0)
            {
                this.Add(ref node.leftChild, value);
            }
            else
            {
                throw new ArgumentException(String.Format("Value {0} already exists.", value));
            }

            this.Skew(ref node);
            this.Split(ref node);
        }

        private bool Remove(ref AANode<T> node, T value)
        {
            if (node == nullNode)
            {
                return (deleted != null);
            }

            int valueComapre = value.CompareTo(node.Value);
            if (valueComapre < 0)
            {
                if (!this.Remove(ref node.leftChild, value))
                {
                    return false;
                }
            }
            else
            {
                if (valueComapre == 0)
                {
                    deleted = node;
                }

                if (!Remove(ref node.rightChild, value))
                {
                    return false;
                }
            }

            if (deleted != null)
            {
                deleted.Value = node.Value;
                deleted = null;
                node = node.rightChild;
            }
            else if (node.leftChild.Level < node.Level - 1 || node.rightChild.Level < node.Level - 1)
            {
                --node.Level;
                if (node.rightChild.Level > node.Level)
                {
                    node.rightChild.Level = node.Level;
                }

                Skew(ref node);
                Skew(ref node.rightChild);
                Skew(ref node.rightChild.rightChild);
                Split(ref node);
                Split(ref node.rightChild);
            }

            return true;
        }

        private void Skew(ref AANode<T> node)
        {
            if (node.Level == node.LeftChild.Level)
            {
                AANode<T> leftChild = node.LeftChild;
                node.LeftChild = leftChild.RightChild;
                leftChild.RightChild = node;
                node = leftChild;
            }
        }

        private void Split(ref AANode<T> node)
        {
            if (node.Level == node.RightChild.RightChild.Level)
            {
                AANode<T> rightChild = node.RightChild;
                node.RightChild = rightChild.LeftChild;
                rightChild.LeftChild = node;
                node = rightChild;
                rightChild.Level++;
            }
        }
    }
}