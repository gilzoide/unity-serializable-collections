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
        [SerializeField] List<T> Entries = new List<T>();

        public void OnAfterDeserialize()
        {
            SerializationUtility.SyncEntriesToSet(Entries, this);
        }

        public void OnBeforeSerialize()
        {
            SerializationUtility.SyncSetToEntries(this, Entries);
        }
    }
}
