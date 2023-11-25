using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProNotes.AppLib.Extensions
{
    public static class ICollectionExtension
    {
        /*
         * Null check on Generics
         * if (EqualityComparer<T>.Default.Equals(obj, default(T)))   
         * {
         *  // obj is null
         * }
         */

        public static bool IsNull<T>(this T obj)
        {
            return EqualityComparer<T>.Default.Equals(obj, default);
        }

        public static bool IsNotNull<T>(this T obj)
        {
            return !obj.IsNull();
        }

        public static void AddIfNotNull<T>(this ICollection<T> coll, T? item)
        {
            if (item.IsNotNull())
            {
                coll.Add(item!);
            }
        }

        public static void AddIfNotExists<T>(this ICollection<T> coll, T? item)
        {
            if (item != null && item.IsNotNull() && !coll.Contains(item))
            {
                coll.Add(item);
            }
        }
    }
}