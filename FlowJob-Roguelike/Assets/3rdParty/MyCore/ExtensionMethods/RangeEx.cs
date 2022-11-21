using System;
using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class RangeEx
    {
        public static int RandomTo(this Range range)
        {
            return range.End.Value.RandomTo() + range.Start.Value;
        }

        public static Vector2Int GetRandomVector2Int(this Range range)
        {
            return new Vector2Int(range.RandomTo(), range.RandomTo());
        }
    }
}