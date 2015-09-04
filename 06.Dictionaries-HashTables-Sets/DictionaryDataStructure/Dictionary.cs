using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryDataStructure
{
    public class Dictionary <TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        public const int DefaultCapacity = 16;
        public const double MaximumFillFactor = 0.75;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public Dictionary(int capacity = DefaultCapacity)
        {
            slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.slots.Length; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return this.Select(x => x.Key); }
        }

        public IEnumerable<TValue> Values
        {
            get { return this.Select(x => x.Value); }
        }

        public void Add(TKey key, TValue value)
        {
            DoubleCapacityIfFillFactorIsReached();

            int slotPosition = GetSlotPosition(key);

            if (this.slots[slotPosition] == null)
            {
                this.slots[slotPosition] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[slotPosition])
            {
                if (Object.Equals(element.Key, key))
                {
                    throw new ArgumentException(String.Format("Key already exists: {0}", key));
                }    
            }

            KeyValue<TKey, TValue> pairToAdd = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotPosition].AddLast(pairToAdd);
            this.Count++;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int position = this.GetSlotPosition(key);

            if (this.slots[position] != null)
            {
                foreach (var keyValuePair in this.slots[position])
                {
                    if (keyValuePair.Key.Equals(key))
                    {
                        return keyValuePair;
                    }
                }
            }

            return null;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);

            if (element == null)
            {
                throw new KeyNotFoundException(String.Format("Key was not found: {0}", key));
            }

            return element.Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element == null)
            {
                value = default(TValue);
                return false;
            }

            value = element.Value;
            return true;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            DoubleCapacityIfFillFactorIsReached();

            int slotPosition = GetSlotPosition(key);

            if (this.slots[slotPosition] == null)
            {
                this.slots[slotPosition] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[slotPosition])
            {
                if (Object.Equals(element.Key, key))
                {
                    element.Value = value;
                    return false;
                }
            }

            KeyValue<TKey, TValue> pairToAdd = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotPosition].AddLast(pairToAdd);
            this.Count++;

            return true;
        }

        public TValue this[TKey key]
        {
            get { return this.Get(key); }

            set
            {
                var element = this.Find(key);

                if (element == null)
                {
                    this.Add(key, value);
                }
                else
                {
                    element.Value = value;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            var element = this.Find(key);
            bool containsKey = element != null;

            return containsKey;
        }

        public bool Remove(TKey key)
        {
            int slotPosition = GetSlotPosition(key);
            var elements = this.slots[slotPosition];
            KeyValue<TKey, TValue> elementToRemove = null;

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    if (element.Key.Equals(key))
                    {
                        elementToRemove = element;
                    }
                }
            }

            if (elementToRemove != null)
            {
                elements.Remove(elementToRemove);
                this.Count--;
                return true;
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in this.slots)
            {
                if (linkedList == null) continue;

                foreach (var keyValue in linkedList)
                {
                    yield return keyValue;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetSlotPosition(TKey key)
        {
            int slotPosition = Math.Abs(key.GetHashCode()) % this.Capacity;
            return slotPosition;
        }

        private void DoubleCapacityIfFillFactorIsReached()
        {
            double fillFactor = ((double)this.Count + 1) / this.Capacity;

            if (fillFactor >= MaximumFillFactor)
            {
                DoubleCapacity();        
            }
        }

        private void DoubleCapacity()
        {
            int newCapacity = this.Capacity * 2;
            LinkedList<KeyValue<TKey, TValue>>[] oldSlots = this.slots;
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[newCapacity];
            this.Count = 0;

            foreach (var linkedList in oldSlots)
            {
                if (linkedList == null) { continue; }

                foreach (var keyValue in linkedList)
                {
                    this.Add(keyValue.Key, keyValue.Value);
                }
            }
        }
    }
}