using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class DebugDraw
    {
        public static void Sphere(Vector3 center, float radius, Color color, float duration = 0)
        {
            CircleInternal(center, Vector3.right, Vector3.up, radius, color, duration);
            CircleInternal(center, Vector3.forward, Vector3.up, radius, color, duration);
            CircleInternal(center, Vector3.right, Vector3.forward, radius, color, duration);
        }

        public static void Box(Vector3 position, Vector3 size, Quaternion orientation, Color color, float duration = 0)
        {
            Vector3 halfSize = size * .5f;

            Vector3 v0 = position + orientation * new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 v1 = position + orientation * new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 v2 = position + orientation * new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            Vector3 v3 = position + orientation * new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            Vector3 v4 = position + orientation * new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
            Vector3 v5 = position + orientation * new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            Vector3 v6 = position + orientation * new Vector3(halfSize.x, halfSize.y, halfSize.z);
            Vector3 v7 = position + orientation * new Vector3(-halfSize.x, halfSize.y, halfSize.z);

            Debug.DrawLine(v0, v1, color, duration, false);
            Debug.DrawLine(v1, v2, color, duration, false);
            Debug.DrawLine(v2, v3, color, duration, false);
            Debug.DrawLine(v3, v0, color, duration, false);

            Debug.DrawLine(v4, v5, color, duration, false);
            Debug.DrawLine(v5, v6, color, duration, false);
            Debug.DrawLine(v6, v7, color, duration, false);
            Debug.DrawLine(v7, v4, color, duration, false);

            Debug.DrawLine(v0, v4, color, duration, false);
            Debug.DrawLine(v1, v5, color, duration, false);
            Debug.DrawLine(v2, v6, color, duration, false);
            Debug.DrawLine(v3, v7, color, duration, false);
        }
        
        static void CircleInternal(Vector3 center, Vector3 v1, Vector3 v2, float radius, Color color, float duration = 0)
        {
            const int segments = 20;
            float arc = Mathf.PI * 2.0f / segments;
            Vector3 p1 = center + v1 * radius;
            for (var i = 1; i <= segments; i++)
            {
                Vector3 p2 = center + v1 * Mathf.Cos(arc * i) * radius + v2 * Mathf.Sin(arc * i) * radius;
                Debug.DrawLine(p1, p2, color, duration);
                p1 = p2;
            }
        }
    }
}