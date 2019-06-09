using System.Text.RegularExpressions;

namespace TextGenerator
{
    public static class Extensons
    {
        public static string RemoveSpecials(this string word)
        {
            string result = Regex.Replace(word, "[^A-Za-z\\s]+", "");
            return result;
        }
    }
}