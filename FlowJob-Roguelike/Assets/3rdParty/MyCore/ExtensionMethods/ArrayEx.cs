using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace Core.Tools.ExtensionMethods
{
    public static class ArrayEx
    {
        public static bool HasElement<T>(this T[] @this, int i)
        {
            return i >= 0 && i < @this.Length;
        }

        public static Object[] RemoveGaps(this Object[] @this)
        {
            return @this.ToList().Where(o => o != null).ToArray();
        }

        public static Object[] Distinct(this Object[] @this)
        {
            return @this.ToList().Distinct().ToArray();
        }

        public static bool Contains<T>(this Object[] @this)
        {
            return @this.OfType<T>().Any();
        }

        public static T Get<T>(this Object[] @this)
        {
            return @this.OfType<T>().FirstOrDefault();
        }

        public static Object[] Add(this Object[] @this, Object item)
        {
            Object[] result = new Object[@this.Length + 1];
            Array.Copy(@this, 0, result, 0, @this.Length);
            result[@this.Length] = item;
            return result;
        }

        public static Object[] Replace(this Object[] @this, Object item)
        {
            List<Object> list = @this.ToList();
            list.RemoveAll(i => i.GetType() == item.GetType());
            list.Add(item);
            return list.ToArray();
        }
    }
}