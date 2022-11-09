using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class RectEx
    {
        public static Bounds ToBounds(this Rect r)
        {
            return new Bounds(r.center, r.size);
        }

        public static Rect WithHeight(this Rect r, float newH)
        {
            return new Rect(r.x, r.y, r.width, newH);
        }
    }
}