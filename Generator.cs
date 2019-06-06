using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Globalization;

namespace TextGenerator
{
    public static class Generator
    {
        private static Random random = new Random();
        private static readonly string SpecialChars = "`~!@#$%^&*()_-+={[}]:;<,>.?/'\\|\"";
        private static readonly string Digits = "0123456789";

        public static void Generate(HashSet<string> words, string outFilepath, int numberOfWords = 100)
        {
            FileStream fileStream = new FileStream(outFilepath, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
                double decision;

                writer.Write($"{ myTI.ToTitleCase(GetRandomWord(words))}");

                int i = 1;
                while(i < numberOfWords)
                {
                    decision = random.NextDouble();
                    if(decision < 0.03)
                    {
                        writer.Write("\n");
                    }
                    else if(decision < 0.18)
                    {
                        writer.Write($" {GetRandomDigit()}");
                    }
                    else if(decision < 0.3)
                    {
                        
                        writer.Write($" {GetSpecialChar()}");
                    }
                    else if(decision < 0.4)
                    {
                        writer.Write($". {myTI.ToTitleCase(GetRandomWord(words))}");
                        i++;
                    }
                    else
                    {
                        writer.Write($" {GetRandomWord(words)}");
                        i++;
                    }
                }
            }
        }

        private static string GetRandomWord(HashSet<string> words)
        {
            return words.ElementAt(random.Next(words.Count));
        }

        private static string GetSpecialChar()
        {
            return SpecialChars.ElementAt(random.Next(SpecialChars.Length)).ToString();
        }

        private static string GetRandomDigit()
        {
            return Digits.ElementAt(random.Next(Digits.Length)).ToString();
        }
    }
}