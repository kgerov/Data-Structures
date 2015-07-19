using System;

namespace _03.ImplementAnArrayBasedStack
{
    public class ArrayStack<T>
    {
        private const int InitialCapacity = 16;
        private T[] elements;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return this.elements[this.Count - 1];
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T element = this.elements[this.Count - 1];
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            T[] arr = new T[this.Count];

            for (int i = this.Count - 1, j = 0; i >= 0; i--, j++)
            {
                arr[j] = this.elements[i];
            }

            return arr;
        }

        private void Grow()
        {
            T[] newArr = new T[2 * this.elements.Length];
            Array.Copy(this.elements, newArr, this.Count);
            this.elements = newArr;
        }
    }
}
