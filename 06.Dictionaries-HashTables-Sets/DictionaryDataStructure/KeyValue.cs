using System;

namespace DictionaryDataStructure
{
    public class KeyValue<TKey, TValue>
    {
        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public override bool Equals(object obj)
        {
            KeyValue<TKey, TValue> castedObj = (KeyValue<TKey, TValue>) obj;

            if (castedObj == null)
            {
                return false;
            }

            bool areEqual = this.Key.Equals(castedObj.Key) && this.Value.Equals(castedObj.Value);

            return areEqual;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());
        }

        private int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }

        public override string ToString()
        {
            string stringValue = String.Format("{0} -> {1}", this.Key, this.Value);
            
            return stringValue;
        }
    }
}