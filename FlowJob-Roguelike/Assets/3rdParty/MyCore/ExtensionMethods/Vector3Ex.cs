using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class Vector3Ex
    {
        public static float ToAngle360(this Vector3 v, Vector3 axis)
        {
            float ang = Vector3.Angle(v, axis);
            Vector3 cross = Vector3.Cross(v, axis);

            if (cross.z > 0)
                ang = 360 - ang;

            return ang;
        }

        public static float ToAngle180(this Vector3 v, Vector3 axis)
        {
            return Vector3.Angle(v, axis);
        }

        public static float ToAngleNegative180(this Vector3 v, Vector3 axis)
        {
            float ang = Vector3.Angle(v, axis);
            Vector3 cross = Vector3.Cross(v, axis);

            if (cross.z > 0)
                ang *= -1;

            return ang;
        }

        public static Vector2 ToV2FromXY(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static Vector2 ToV2FromXZ(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        public static Vector3 Add(this Vector3 v3, float number)
        {
            v3.Set(v3.x + number, v3.y + number, v3.z + number);
            return v3;
        }

        public static Vector3 Add(this Vector3 v3, Vector3 other)
        {
            v3.Set(v3.x + other.x, v3.y + other.y, v3.z + other.z);
            return v3;
        }

        public static Vector3 Mult(this Vector3 v3, Vector3 other)
        {
            v3.Set(v3.x * other.x, v3.y * other.y, v3.z * other.z);
            return v3;
        }

        public static Vector3 Div(this Vector3 v3, Vector3 other)
        {
            v3.Set(v3.x / other.x, v3.y / other.y, v3.z / other.z);
            return v3;
        }

        public static Vector3 WithX(this Vector3 v3, float x)
        {
            v3.Set(x, v3.y, v3.z);
            return v3;
        }

        public static Vector3 WithY(this Vector3 v3, float y)
        {
            v3.Set(v3.x, y, v3.z);
            return v3;
        }

        public static Vector3 WithZ(this Vector3 v3, float z)
        {
            v3.Set(v3.x, v3.y, z);
            return v3;
        }

        public static Vector3 Abs(this Vector3 a)
        {
            a.Set(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z));
            return a;
        }

        public static Vector3 Round(this Vector3 v)
        {
            return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
        }

        public static Vector3 RandomTo(this Vector3 v)
        {
            return new Vector3(v.x.RandomTo(), v.y.RandomTo(), v.z.RandomTo());
        }
        
        public static Vector3 ClampMagnitude(this Vector3 v, float length)
        {
            if (v.magnitude > length)
            {
                v = v.normalized * length;    
            }
            
            return v;
        }
    }
}