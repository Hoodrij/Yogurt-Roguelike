namespace Core.Tools.ExtensionMethods
{
    public static class StringFormatEx
    {
        public static string ToCommaString(this ulong number)
        {
            return $"{number:n0}";
        }

        public static string ToCommaString(this double number)
        {
            return $"{number:0.##}";
        }

        public static string ToCommaString(this int number)
        {
            return $"{number:n0}";
        }

        public static string ToCommaString(this long number)
        {
            return $"{number:n0}";
        }

        public static string ToCommaString(this float number)
        {
            return $"{number:0.##}";
        }
    }
}