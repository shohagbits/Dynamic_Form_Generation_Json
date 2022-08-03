using System.Text.RegularExpressions;

namespace Dynamic_Form_Generation_Json
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str, int tIndex)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.Length > 1)
            {
                var x = str.Replace(" ", "");
                x = x + tIndex;
                if (x.Length == 0) return "null";
                x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                    m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
                return char.ToLower(x[0]) + x.Substring(1);
            }
            return str.ToLowerInvariant();
        }
    }
}
