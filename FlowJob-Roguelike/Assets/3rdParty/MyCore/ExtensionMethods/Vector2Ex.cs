using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class Vector2Ex
    {
         public static Vector2 Rotate(ref this Vector2 v, float degrees) 
         {
             return Quaternion.Euler(0, 0, degrees) * v;
         }
         
        public static float ToAngle360(this Vector2 v2, Vector2 axis)
        {
            float ang = Vector2.Angle(v2, axis);
            Vector3 cross = Vector3.Cross(v2, axis);

            if (cross.z > 0)
                ang = 360 - ang;

            return ang;
        }

        public static float ToAngle180(this Vector2 v2, Vector2 axis)
        {
            return Vector2.Angle(v2, axis);
        }

        public static float ToAngleNegative180(this Vector2 v2, Vector2 axis)
        {
            float ang = Vector2.Angle(v2, axis);
            Vector3 cross = Vector3.Cross(v2, axis);

            if (cross.z > 0)
                ang *= -1;

            return ang;
        }

        public static Vector2 GetRandomDir()
        {
            return new Vector2(200.RandomTo() - 100, 200.RandomTo() - 100).normalized;
        }

        public static Vector3 ToV3XY(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }

        public static Vector3 ToV3XZ(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }

        public static Vector2 Add(this Vector2 v2, float number)
        {
            return new Vector2(v2.x + number, v2.y + number);
        }

        public static Vector2 Add(this Vector2 v2, Vector2 other)
        {
            return new Vector2(v2.x + other.x, v2.y + other.y);
        }

        public static Vector2 Mult(this Vector2 v, Vector3 other)
        {
            return new Vector2(v.x * other.x, v.y * other.y);
        }

        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        public static Vector2 Abs(this Vector2 a)
        {
            a.Set(Mathf.Abs(a.x), Mathf.Abs(a.y));
            return a;
        }

        public static Vector2 Round(this Vector2 v)
        {
            return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y));
        }

        public static Vector2 RandomTo(this Vector2 v)
        {
            return new Vector2(v.x.RandomTo(), v.y.RandomTo());
        }
    }
}