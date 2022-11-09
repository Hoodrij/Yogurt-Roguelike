namespace Core.Tools.ExtensionMethods
{
    public static class IntEx
    {
        public static int RandomTo(this int i)
        {
            return UnityEngine.Random.Range(0, i);
        }

        public static bool ToBool(this int i)
        {
            return i == 1;
        }
        
        public static int Remap(this int value, int from1, int to1, int from2, int to2)
        {
            return (int) (((float) (value - from1)) / (to1 - from1) * (to2 - from2) + from2);
        }

        public static int Remap01(this int value, int from1, int to1)
        {
            return (value - from1) / (to1 - from1);
        }
    }
}