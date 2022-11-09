using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class FloatEx
    {
        public static float Abs(this float f)
        {
            return Mathf.Abs(f);
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Remap01(this float value, float from1, float to1)
        {
            return (value - from1) / (to1 - from1);
        }

        public static Vector3 ToV3(this float f)
        {
            return Vector3.one * f;
        }

        public static Vector3 ToV2(this float f)
        {
            return Vector2.one * f;
        }

        public static float RandomTo(this float f)
        {
            return Random.Range(0, f);
        }

        public static float WithRandomSign(this float f)
        {
            return f * (true.RandomBool() ? 1 : -1);
        }

        public static Vector2 ToDirection(this float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * axis;
        }

        public static string FloatToTime(this float toConvert, string format)
        {
            switch (format)
            {
                case "00.0":
                    return $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor(toConvert * 10 % 10):0}"; //miliseconds
                case "#0.0":
                    return $"{Mathf.Floor(toConvert) % 60:#0}:{Mathf.Floor(toConvert * 10 % 10):0}"; //miliseconds
                case "00.00":
                    return $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor(toConvert * 100 % 100):00}"; //miliseconds
                case "00.000":
                    return $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor(toConvert * 1000 % 1000):000}"; //miliseconds
                case "#00.000":
                    return
                        $"{Mathf.Floor(toConvert) % 60:#00}:{Mathf.Floor(toConvert * 1000 % 1000):000}"; //miliseconds
                case "#0:00":
                    return $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}"; //seconds
                case "#00:00":
                    return $"{Mathf.Floor(toConvert / 60):#00}:{Mathf.Floor(toConvert) % 60:00}"; //seconds
                case "0:00.0":
                    return
                        $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 10 % 10):0}"; //miliseconds
                case "#0:00.0":
                    return
                        $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 10 % 10):0}"; //miliseconds
                case "0:00.00":
                    return
                        $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 100 % 100):00}"; //miliseconds
                case "#0:00.00":
                    return
                        $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 100 % 100):00}"; //miliseconds
                case "0:00.000":
                    return
                        $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 1000 % 1000):000}"; //miliseconds
                case "#0:00.000":
                    return
                        $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor(toConvert * 1000 % 1000):000}"; //miliseconds
            }

            return "error";
        }
    }
}