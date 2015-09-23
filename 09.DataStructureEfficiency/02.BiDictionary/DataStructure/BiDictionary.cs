using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.BiDictionary.DataStructure
{
    public class BiDictionary<TKey1, TKey2, TValue>
    {
        private Dictionary<TKey1, List<TValue>> keyOneValues;
        private Dictionary<TKey2, List<TValue>> keyTwoValues;
        private Dictionary<Tuple<TKey1, TKey2>, List<TValue>> combinedKeyValues;

        public BiDictionary()
        {
            keyOneValues = new Dictionary<TKey1, List<TValue>>();
            keyTwoValues = new Dictionary<TKey2, List<TValue>>();
            combinedKeyValues = new Dictionary<Tuple<TKey1, TKey2>, List<TValue>>();
        }

        public int Count 
        {
            get { return this.keyOneValues.Count; }
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            if (!this.keyOneValues.ContainsKey(key1))
            {
                keyOneValues.Add(key1, new List<TValue>());
            }

            if (!this.keyTwoValues.ContainsKey(key2))
            {
                keyTwoValues.Add(key2, new List<TValue>());
            }

            var combinedKey = new Tuple<TKey1, TKey2>(key1, key2);

            if (!this.combinedKeyValues.ContainsKey(combinedKey))
            {
                combinedKeyValues.Add(combinedKey, new List<TValue>());
            }

            keyOneValues[key1].Add(value);
            keyTwoValues[key2].Add(value);
            combinedKeyValues[combinedKey].Add(value);
        }

        public IEnumerable<TValue> Find(TKey1 key1, TKey2 key2)
        {
            var combinedKey = new Tuple<TKey1, TKey2>(key1, key2);

            if (!this.combinedKeyValues.ContainsKey(combinedKey))
            {
                yield break;
            }

            foreach (var value in this.combinedKeyValues[combinedKey])
            {
                yield return value;
            }
        }

        public IEnumerable<TValue> FindByKey1(TKey1 key1)
        {
            if (!this.keyOneValues.ContainsKey(key1))
            {
                yield break;    
            }

            foreach (var value in this.keyOneValues[key1])
            {
                yield return value;
            }
        }

        public IEnumerable<TValue> FindByKey2(TKey2 key2)
        {
            if (!this.keyTwoValues.ContainsKey(key2))
            {
                yield break;
            }

            foreach (var value in this.keyTwoValues[key2])
            {
                yield return value;
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            if (!this.keyOneValues.ContainsKey(key1) || !this.keyTwoValues.ContainsKey(key2))
            {
                return false;
            }

            IEnumerable<TValue> valuesForRemoval = this.Find(key1, key2);

            // Remove values from Key One Dictionary and Key Two Dictionary

            this.keyOneValues[key1].RemoveAll(x => valuesForRemoval.Contains(x));
            this.keyTwoValues[key2].RemoveAll(x => valuesForRemoval.Contains(x));

            if (keyOneValues[key1].Count == 0)
            {
                keyOneValues.Remove(key1);
            }

            if (keyTwoValues[key2].Count == 0)
            {
                keyTwoValues.Remove(key2);
            }

            // Remove Values from Combined Dictionary

            combinedKeyValues.Remove(new Tuple<TKey1, TKey2>(key1, key2));

            return true;
        }
    }
}