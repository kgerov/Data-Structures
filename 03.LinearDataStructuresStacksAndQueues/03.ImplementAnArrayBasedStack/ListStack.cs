using System;

namespace _03.ImplementAnArrayBasedStack
{
    public class ListStack<T>
    {
        private Node<T> firstNode;
        public int Count { get; private set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elemenets in stack");
            }

            return this.firstNode.Value;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elemenets in stack");
            }

            T value = this.firstNode.Value;
            this.firstNode = firstNode.NextNode;
            this.Count--;

            return value;
        }

        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            Node<T> current = this.firstNode;
            int index = 0;

            while (current != null)
            {
                arr[index] = current.Value;
                current = current.NextNode;

                index++;
            }

            return arr;
        }

        private class Node<T>
        {
            private T value;

            public Node(T value, Node<T> nextNode = null)
            {
                this.value = value;
                this.NextNode = nextNode;
            }

            public T Value {
                get { return this.value; }
            }

            public Node<T> NextNode { get; set; }

        }
    }
}
