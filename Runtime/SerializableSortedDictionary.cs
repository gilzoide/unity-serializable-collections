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
        [SerializeField] List<SerializableKeyValuePair<TKey, TValue>> Entries =
            new List<SerializableKeyValuePair<TKey, TValue>>();

        public void OnAfterDeserialize()
        {
            SerializationUtility.SyncEntriesToDict(Entries, this);
        }

        public void OnBeforeSerialize()
        {
            SerializationUtility.SyncDictToEntries(this, Entries);
        }
    }
}
