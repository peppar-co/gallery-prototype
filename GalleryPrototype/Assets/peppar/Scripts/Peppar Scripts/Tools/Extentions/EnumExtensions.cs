using System;
using UnityEngine;

namespace peppar
{
    public static class EnumExtensions
    {
        public static T GetEnum<T>(this string instance) where T : struct, IConvertible
        {
            if (typeof(T).IsEnum != true)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (System.Enum.IsDefined(typeof(T), instance))
            {
                return (T)System.Enum.Parse(typeof(T), instance);
            }
            else
            {
                Debug.LogWarning("Value <" + instance + "> not defined in Enum " + typeof(T).Name);
                return default(T);
            }
        }
    }
}
