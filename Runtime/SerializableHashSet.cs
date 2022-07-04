using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.SerializableCollections
{
    [Serializable]
    public class SerializableHashSet<T> : HashSet<T>,
        ISerializationCallbackReceiver,
        ISingleFieldDrawable
    {
        [SerializeField] List<T> Entries = new List<T>();

        public void OnAfterDeserialize()
        {
            OnDeserialization(this);
            SerializationUtility.SyncEntriesToSet(Entries, this);
        }

        public void OnBeforeSerialize()
        {
            SerializationUtility.SyncSetToEntries(this, Entries);
        }
    }
}
