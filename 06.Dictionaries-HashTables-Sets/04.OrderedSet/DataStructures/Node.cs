namespace _04.OrderedSet.DataStructures
{
    using System;

    public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
    {
        public Node(T value, Node<T> parent = null)
        {
            this.Value = value;
            this.Parent = parent;
        }

        public T Value { get; set; }

        public Node<T> Parent { get; set; }

        public Node<T> BiggerChild { get; set; }

        public Node<T> SmallerChild { get; set; }

        public bool HasChildren()
        {
            if (this.BiggerChild != null || this.SmallerChild != null)
            {
                return true;
            }

            return false;
        }

        public Node<T> GetBiggestChild()
        {
            if (this.BiggerChild != null)
            {
                return this.BiggerChild;
            }

            if (this.SmallerChild != null)
            {
                return this.SmallerChild;
            }

            return null;
        }

        public int CompareTo(Node<T> other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}