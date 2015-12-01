using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace peppar
{
    public static class PrintExtensions
    {
        public static string PrintClass(this Object instance, bool publicOnly = true)
        {
            var builder = new StringBuilder();
            if (instance == null)
            {
                builder.Append("[Class=null]");
            }
            else
            {
                try
                {
                    foreach (var p in instance.GetType().GetProperties(publicOnly ? BindingFlags.Instance | BindingFlags.Public : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        builder.AppendFormat("+ [{0}={1}]\n", p.Name, PrintObject(p.GetValue(instance, null)));
                    }
                    foreach (var p in instance.GetType().GetFields(publicOnly ? BindingFlags.Instance | BindingFlags.Public : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        builder.AppendFormat("+ [{0}={1}]\n", p.Name, PrintObject(p.GetValue(instance)));
                    }
                }
                catch
                {
                    builder.AppendFormat("[Class={0}]\n", instance.GetType().Name);
                }
            }
            return builder.ToString();
        }

        private static string PrintObject(object value)
        {
            if (value == null)
            {
                return "null";
            }
            else if (value is string)
            {
                return value as string;
            }
            else if (value.GetType().IsValueType)
            {
                return value.ToString();
            }
            else if (value is ICollection)
            {
                return PrintCollection(value);
            }
            else
            {
                return value.ToString();
            }
        }

        public static string PrintCollection(this IEnumerable collection, int maxItems = 10)
        {
            var b = new StringBuilder();
            var iter = collection.GetEnumerator();
            int count = 0;

            b.Append("|");
            while (iter.MoveNext() && count++ < maxItems)
            {
                if (iter.Current != null)
                {
                    b.AppendFormat("{0}|", iter.Current);
                }
                else
                {
                    b.Append("null|");
                }
            }
            return b.ToString();
        }

        private static string PrintCollection(params object[] args)
        {
            var b = new StringBuilder();

            if (args != null)
            {
                if (args.Length == 0)
                {
                    b.Append("EMPTY");
                }
                else
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        object o = args[i];
                        if (o != null)
                        {
                            if (o is IEnumerable)
                            {
                                var collection = o as IEnumerable;
                                var iter = collection.GetEnumerator();
                                int j = 0;

                                b.Append("|");
                                while (iter.MoveNext() && j < 50)
                                {
                                    if (iter.Current != null)
                                        b.AppendFormat("{0}|", iter.Current);
                                    else
                                        b.Append("null|");
                                    j++;
                                }

                                if (j == 0)
                                {
                                    b.Append("empty|");
                                }
                                else if (j == 50)
                                {
                                    b.Append("...");
                                }
                            }
                            else
                            {
                                b.AppendFormat("{0}", o);
                            }
                        }
                        else
                        {
                            b.AppendFormat("null");
                        }
                        if (i < args.Length - 1)
                        {
                            b.AppendFormat(", ");
                        }
                    }
                }
            }
            else
            {
                b.Append("NULL");
            }

            return b.ToString();
        }
    }
}
