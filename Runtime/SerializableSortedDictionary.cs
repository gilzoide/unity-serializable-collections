using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.SerializableCollections
{
    [Serializable]
    public class SerializableSortedDictionary<TKey, TValue> : SortedDictionary<TKey, TValue>,
        ISerializationCallbackReceiver,
        ISingleFieldDrawable
    {
        [SerializeField] List<SerializableKeyValuePair<TKey, TValue>> SerializedEntries =
            new List<SerializableKeyValuePair<TKey, TValue>>();

        public void OnAfterDeserialize()
        {
            SerializationUtility.SyncEntriesToDict(SerializedEntries, this);
        }

        public void OnBeforeSerialize()
        {
            SerializationUtility.SyncDictToEntries(this, SerializedEntries);
        }
    }
}
