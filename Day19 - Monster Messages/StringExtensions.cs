using System;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static Tuple<string, string> SplitIntoTwo(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] parts = str.Split(separator, options);
            if(parts.Length != 2)
                throw new Exception();
            return new Tuple<string, string>(parts[0], parts[1]);
        }

        public static Tuple<string, string> SplitIntoTwo(this string str, char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] parts = str.Split(separator, options);
            if(parts.Length != 2)
                throw new Exception();
            return new Tuple<string, string>(parts[0], parts[1]);
        }
    }
}