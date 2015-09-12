using System;

namespace _01.AATree.DataStructure
{
    public class AANode<T> where T : IComparable<T>
    {
        internal AANode<T> leftChild;
        internal AANode<T> rightChild;
 
        public AANode()
        {
            this.LeftChild = this;
            this.RightChild = this;
            this.Level = 0;
        }

        public AANode(T value, AANode<T> nullNode)
        {
            this.Value = value;
            this.RightChild = nullNode;
            this.LeftChild = nullNode;
            this.Level = 1;
        }

        public T Value { get; set; }

        public AANode<T> LeftChild
        {
            get { return this.leftChild; }
            set { this.leftChild = value; }
        }

        public AANode<T> RightChild
        {
            get { return this.rightChild; }
            set { this.rightChild = value; }
        }

        public int Level { get; set; } 
    }
}