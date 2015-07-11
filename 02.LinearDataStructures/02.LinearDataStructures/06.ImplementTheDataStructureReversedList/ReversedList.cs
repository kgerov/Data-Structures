using System;
using System.Collections;
using System.Collections.Generic;

namespace _06.ImplementTheDataStructureReversedList
{
    public class ReversedList<T> : IEnumerable<T>
    {
        private T[] list;
        private int capacity;
        private int count;

        public ReversedList(int capacity = 4)
        {
            this.list = new T[capacity];
            this.Count = 0;
            this.Capacity = capacity;
        }

        public void Add(T element)
        {
            if (this.Count == this.Capacity-1)
            {
                this.DoubleCapacity();
            }

            this.list[this.count] = element; 
            this.count++;
        }

        public void Remove(int index)
        {

            T forRemoval = this.list[index];
            int nextIndex = this.count - index;

            for (int i = nextIndex; i < this.count; i++)
            {
                this.list[i - 1] = this.list[i];
            }

            this.count--;
        }

        public T this[int index]
        {
            get
            {
                this.CheckIfValidIndex(index);
                return this.list[this.Count - 1 - index];
            }

            set
            {
                this.CheckIfValidIndex(index);
                this.list[this.Count - 1 - index] = value;
            }
        }

        public int Count
        {
            get { return this.count; }

            private set { this.count = value; }
        }

        public int Capacity
        {
            get { return this.capacity; }

            private set { this.capacity = value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (long i = this.count - 1; i >= 0; i--)
            {
                yield return this.list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void DoubleCapacity()
        {
            this.capacity *= 2;
            T[] newArr = new T[this.capacity];
            int index = 0;

            foreach (var element in this.list)
            {
                newArr[index] = element;
                index++;
            }

            this.list = newArr;
        }

        private void CheckIfValidIndex(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new IndexOutOfRangeException("Index should be bigger than zero and smaller then the count of the reversed list");
            }
        }
    }
}
