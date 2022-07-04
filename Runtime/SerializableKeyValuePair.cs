using System;
using System.Collections.Generic;

namespace Gilzoide.SerializableCollections
{
    [Serializable]
    public struct SerializableKeyValuePair<TKey, TValue> : IKeyValuePairDrawable
    {
        public TKey Key;
        public TValue Value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = Key;
            value = Value;
        }

        public static implicit operator SerializableKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> kvp)
        {
            return new SerializableKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value);
        }
        public static implicit operator KeyValuePair<TKey, TValue>(SerializableKeyValuePair<TKey, TValue> kvp)
        {
            return new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value);
        }
    }
}
