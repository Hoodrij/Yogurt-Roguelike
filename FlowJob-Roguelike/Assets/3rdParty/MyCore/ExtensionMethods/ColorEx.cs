using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class ColorEx
    {
        public static Color WithAlpha(this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, a);
        }
    }
}