using System.Collections.Generic;
using System.Linq;

namespace Gilzoide.SerializableCollections
{
    public static class SerializationUtility
    {
        public static void SyncEntriesToDict<TKey, TValue>(
            IList<SerializableKeyValuePair<TKey, TValue>> entries,
            IDictionary<TKey, TValue> dict)
        {
            dict.Clear();
            foreach ((TKey key, TValue value) in entries)
            {
                dict[key] = value;
            }
#if !UNITY_EDITOR
            entries.Clear();
#endif
        }

        public static void SyncDictToEntries<TKey, TValue>(
            IDictionary<TKey, TValue> dict,
            IList<SerializableKeyValuePair<TKey, TValue>> entries)
        {
#if UNITY_EDITOR
            var existingKeys = new HashSet<TKey>(entries.Select(e => e.Key));
            foreach (var entry in dict)
            {
                if (!existingKeys.Contains(entry.Key))
                {
                    entries.Add(entry);
                }
            }
#else
            entries.Clear();
            foreach (var entry in dict)
            {
                entries.Add(entry);
            }
#endif
        }

        public static void SyncEntriesToSet<T>(IList<T> entries, ISet<T> set)
        {
            set.Clear();
            set.UnionWith(entries);
#if !UNITY_EDITOR
            entries.Clear();
#endif
        }

        public static void SyncSetToEntries<T>(ISet<T> set, List<T> entries)
        {
#if UNITY_EDITOR
            var missingValues = new HashSet<T>(set);
            missingValues.ExceptWith(entries);
            entries.AddRange(missingValues);
#else
            entries.Clear();
            entries.AddRange(set);
#endif
        }
    }
}
