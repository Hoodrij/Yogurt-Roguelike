using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class Vector2IntEx
    {
        public static Vector2Int Normalized(this Vector2Int i)
        {
            if (i == Vector2Int.zero) return Vector2Int.zero;
            
            int absX = Mathf.Abs(i.x);
            int absY = Mathf.Abs(i.y);

            if (absX > absY)
            {
                return i.x > 0 ? Vector2Int.right : Vector2Int.left;
            }
            else
            {
                return i.y > 0 ? Vector2Int.up : Vector2Int.down;
            }
        } 
    }
}