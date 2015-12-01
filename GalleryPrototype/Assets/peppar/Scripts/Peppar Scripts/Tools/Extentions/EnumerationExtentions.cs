using System.Collections.Generic;
using System.Linq;

namespace peppar
{
    public static class EnumerationExtensions
    {
        public static void AddRange<T>(this Queue<T> instance, IEnumerable<T> enumeration)
        {
            foreach (var e in enumeration)
            {
                instance.Enqueue(e);
            }
        }

        public static void AddRange<T>(this HashSet<T> instance, IEnumerable<T> enumeration)
        {
            foreach (var e in enumeration)
            {
                instance.Add(e);
            }
        }

        public static int Count<T>(this IEnumerable<T> instance)
        {
            ICollection<T> collection = instance as ICollection<T>;
            if (collection != null)
            {
                return collection.Count;
            }

            int result = 0;
            using (IEnumerator<T> enumerator = instance.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    result++;
                }
            }
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> instance, System.Action<T> action)
        {
            foreach (var item in instance)
            {
                action(item);
            }
        }

        public static void ForEachBreakable<T>(this IEnumerable<T> instance, System.Func<T, bool> func)
        {
            foreach (var item in instance)
            {
                if (func(item))
                {
                    break;
                }
            }
        }

        public static bool TryGetValueWithLowerKey<TValue>(this IDictionary<string, TValue> instance, string key, out TValue value)
        {
            return instance.TryGetValue(key.ToLower(), out value);
        }

        public static void AddWithLowerKey<TValue>(this IDictionary<string, TValue> instance, string key, TValue value)
        {
            instance.Add(key.ToLower(), value);
        }

        public static bool AnyWithLowerKey<TValue>(this IDictionary<string, TValue> instance, string key)
        {
            return instance.Any(item => item.Key == key.ToLower());
        }

        internal static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key)
        {
            TValue val;
            if (instance.TryGetValue(key, out val))
                return val;
            else
                return default(TValue);
        }
    }

}
