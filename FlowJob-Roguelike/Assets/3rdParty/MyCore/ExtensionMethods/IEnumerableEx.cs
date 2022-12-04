using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tools.ExtensionMethods
{
    public static class IEnumerableEx
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(T item in source)
            {
                action(item);
            }
        }

        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        public static bool HasIndex<T>(this IEnumerable<T> list, int i)
        {
            return i > -1 && i < list.Count();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }

        public static ICollection<T> AddAndGet<T>(this ICollection<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            int index = list.Count().RandomTo();
            return list.ElementAt(index);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> list, T exception)
        {
            return list.Where(t => !t.Equals(exception));
        }
    }
}