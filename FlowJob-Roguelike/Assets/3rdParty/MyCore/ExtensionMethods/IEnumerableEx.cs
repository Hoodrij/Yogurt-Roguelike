using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tools.ExtensionMethods
{
    public static class IEnumerableEx
    {
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            IEnumerable<T> enumerable = list.ToList();
            
            if (enumerable.IsEmpty()) 
                return default;
            int index = enumerable.Count().RandomTo();
            return enumerable.ElementAt(index);
        }
    }
}