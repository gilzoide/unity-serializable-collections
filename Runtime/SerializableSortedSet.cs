using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.SerializableCollections
{
    [Serializable]
    public class SerializableSortedSet<T> : SortedSet<T>,
        ISerializationCallbackReceiver,
        ISingleFieldDrawable
    {
        [SerializeField] List<T> SerializedEntries = new List<T>();

        public void OnAfterDeserialize()
        {
            SerializationUtility.SyncEntriesToSet(SerializedEntries, this);
        }

        public void OnBeforeSerialize()
        {
            SerializationUtility.SyncSetToEntries(this, SerializedEntries);
        }
    }
}
