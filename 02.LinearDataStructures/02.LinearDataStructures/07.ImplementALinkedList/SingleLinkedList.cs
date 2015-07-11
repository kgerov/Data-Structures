using System;
using System.Collections;
using System.Collections.Generic;

namespace _07.ImplementALinkedList
{
    public class SingleLinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> firstNode;
        private int count;

        public SingleLinkedList()
        {
            this.count = 0;
        }

        public void Add(T element)
        {
            ListNode<T> newNode = new ListNode<T>(element);

            if (count == 0)
            {
                this.firstNode = newNode;
            }
            else
            {
                this[this.count-1].NextNode = newNode;
            }

            count++;
        }

        public void Remove(int index)
        {
            ListNode<T> forRemoval = this[index];

            if (index == 0)
            {
                if (this.count > 1)
                {
                    this.firstNode = this[index + 1];
                }
                else
                {
                    this.firstNode = null;
                }
            }
            else
            {
                ListNode<T> prevNode = this[index - 1];

                if (index == this.count - 1)
                {
                    prevNode.NextNode = null;
                }
                else
                {
                    prevNode.NextNode = this[index + 1];
                }
            }

            this.count--;
        }

        private ListNode<T> this[int index]
        {
            get
            {
                this.CheckIfValidIndex(index);

                int currentIndex = 0;
                ListNode<T> currentNode = this.firstNode;

                while (currentNode != null)
                {
                    if (currentIndex == index)
                    {
                        return currentNode;
                    }

                    currentNode = currentNode.NextNode;
                    currentIndex++;
                }

                throw new KeyNotFoundException();
            }

            set
            {
                this.CheckIfValidIndex(index);

                int currentIndex = 0;
                ListNode<T> currentNode = this.firstNode;

                while (currentNode != null)
                {
                    if (currentIndex == index)
                    {
                        currentNode = value;
                        break;
                    }

                    currentNode = currentNode.NextNode;
                    currentIndex++;
                }
            }
        }

        public int FirstIndexOf(T Item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this[i].Value.Equals(Item))
                {
                    return i;
                }
            }

            return -1;
        }

        public int LastIndexOf(T Item)
        {
            int index = -1;

            for (int i = 0; i < this.count; i++)
            {
                if (this[i].Value.Equals(Item))
                {
                    index = i;
                }
            }

            return index;
        }

        public int Count
        {
            get { return this.count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T> currentNode = this.firstNode;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckIfValidIndex(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new IndexOutOfRangeException("Index has to be equal or bigger than zero and smaller than the size of the list.");
            }
        }
    }
}
