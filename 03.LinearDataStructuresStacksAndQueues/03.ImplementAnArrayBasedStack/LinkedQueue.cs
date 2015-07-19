using System;

namespace _03.ImplementAnArrayBasedStack
{
    public class LinkedQueue<T>
    {
        private QueueNode<T> firstNode;
        private QueueNode<T> lastNode;
        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            QueueNode<T> newNode = new QueueNode<T>(element);

            if (this.Count == 0)
            {
                this.firstNode = newNode;
                this.lastNode = newNode;
            }
            else if (this.Count == 1)
            {
                this.firstNode.NextNode = newNode;
                newNode.PrevNode = this.firstNode;
                newNode.NextNode = null;
                this.lastNode = newNode;
            }
            else
            {
                this.lastNode.NextNode = newNode;
                newNode.PrevNode = this.lastNode;
                newNode.NextNode = null;
                this.lastNode = newNode;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue has no elements");
            }

            T returnValue = this.firstNode.Value;

            this.firstNode = this.firstNode.NextNode;
            if (this.firstNode != null)
            {
                this.firstNode.PrevNode = null;
            }

            this.Count--;
            return returnValue;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue has no elements");
            }

            return this.firstNode.Value;
        }

        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            QueueNode<T> current = this.firstNode;
            int index = 0;

            while (current != null)
            {
                arr[index] = current.Value;
                current = current.NextNode;
                index++;
            }

            return arr;
        }

        private class QueueNode<T>
        {
            public QueueNode(T value)
            {
                this.Value = value;
            }
            public T Value { get; private set; }
            public QueueNode<T> NextNode { get; set; }
            public QueueNode<T> PrevNode { get; set; }
        }
    }
}