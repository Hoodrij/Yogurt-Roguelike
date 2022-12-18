using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class Vector2IntEx
    {
        public static Vector2Int Normalized(this Vector2Int v)
        {
            if (v == Vector2Int.zero) return Vector2Int.zero;
            
            int absX = Mathf.Abs(v.x);
            int absY = Mathf.Abs(v.y);

            if (absX > absY)
            {
                return v.x > 0 ? Vector2Int.right : Vector2Int.left;
            }
            else
            {
                return v.y > 0 ? Vector2Int.up : Vector2Int.down;
            }
        } 
        
        public static bool IsNormalized(this Vector2Int v)
        {
            return v.x == 0 && Mathf.Abs(v.y) == 1
                   || Mathf.Abs(v.x) == 1 && v.y == 0;
        }
        
        public static Vector3 ToV3XY(this Vector2Int v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }
    }
}