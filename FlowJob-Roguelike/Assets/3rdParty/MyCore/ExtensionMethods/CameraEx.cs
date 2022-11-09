using System;
using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class CameraEx
    {
        public static Bounds OrthographicBounds(this Camera cam)
        {
            return cam.OrthographicBounds(transform => transform.position);
        }

        public static Bounds LocalOrthographicBounds(this Camera cam)
        {
            return cam.OrthographicBounds(transform => transform.localPosition);
        }

        private static Bounds OrthographicBounds(this Camera cam, Func<Transform, Vector3> positionGetter)
        {
            float height = cam.orthographicSize * 2;
            return new Bounds(positionGetter(cam.transform),
                new Vector2(height * Screen.width / Screen.height, height));
        }

        public static Vector2 GetSize(this Camera cam)
        {
            return new Vector2(Camera.main.orthographicSize * Camera.main.aspect * 2, Camera.main.orthographicSize * 2);
        }
    }
}