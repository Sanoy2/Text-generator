using System.Collections.Generic;
using System.IO;

namespace TextGenerator
{
    public static class Reader
    {
        public static HashSet<string> GetWords(string filepath)
        {
            var words = new HashSet<string>();
            string word;
        
            FileStream fileStream = new FileStream(filepath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while(!reader.EndOfStream)
                {
                    word = reader.ReadLine();
                    words.Add(word.Trim().ToLower());
                }
            }

            return words;
        }
    }
}