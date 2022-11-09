using Random = UnityEngine.Random;

namespace Core.Tools.ExtensionMethods
{
    public static class BoolEx
    {
        public static int To01(this bool value)
        {
            return value ? 1 : 0;
        }

        public static bool RandomBool(this bool b)
        {
            return Random.value > 0.5f;
        }
    }
}