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
        
        public static Vector3 ToV3XY(this Vector2Int v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }

        public static Vector3 ToV3XZ(this Vector2Int v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }

        public static Vector2Int RandomTo(this Vector2Int v2)
        {
            return new Vector2Int(v2.x.RandomTo(), v2.y.RandomTo());
        }
    }
}