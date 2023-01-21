using System.Collections.Generic;
using System.Linq;

namespace Core.Tools.ExtensionMethods
{
    public static class IEnumerableEx
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            List<T> list = enumerable.ToList();
            
            if (list.IsEmpty()) 
                return default;
            int index = list.Count().RandomTo();
            return list.ElementAt(index);
        }
    }
}