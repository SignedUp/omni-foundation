using System;
using System.Collections.Generic;

namespace Omniscient.Foundation
{
    public static class SilverlightExtensions
    {
        public static bool Exists<T>(this List<T> list, Predicate<T> predicate)
        {
            foreach (T item in list)
            {
                if (predicate(item)) return true;
            }

            return false;
        }

        public static T Find<T>(this List<T> list, Predicate<T> predicate)
        {
            foreach (T item in list)
            {
                if (predicate(item)) return item;
            }

            return default(T);
        }
    }
}
