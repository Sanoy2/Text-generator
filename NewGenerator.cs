using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace TextGenerator
{
    public class NewGenerator
    {
        private static readonly string SpecialChars = "`~!@#$%^&*()_-+={[}]:;<,>?/'\\|\"";
        private static readonly string Digits = "0123456789";

        private static readonly int minSentenceLength = 2;
        private static readonly int maxSentenceLength = 30;

        private readonly HashSet<string> words;

        public NewGenerator(HashSet<string> words)
        {
            this.words = words;
        }

        public void Generate(string outFilepath, int numberOfWords = 100, int numberOfThreads = 8)
        {
            var intsLeft = numberOfWords;
            var sentences = new List<string>();
            var numbersOfWords = new List<int>();
            int wordsForTask = 0;
            var random = new Random();

            FileStream fileStream = new FileStream(outFilepath, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                while(intsLeft > 0)
                {
                    // numbersOfWords.Clear();
                    numbersOfWords = new List<int>();
                    for (int i = 0; i < numberOfThreads; i++)
                    {
                        wordsForTask = random.Next(minSentenceLength, maxSentenceLength);
                        if(intsLeft - wordsForTask <= minSentenceLength)
                        {
                            wordsForTask = intsLeft;
                            intsLeft = 0;
                            numbersOfWords.Add(wordsForTask);
                            break;
                        }

                        intsLeft -= wordsForTask;
                        numbersOfWords.Add(wordsForTask);
                    }

                    Parallel.ForEach(numbersOfWords, numberForSingleThread =>
                    {
                        writer.Write(GetSentence(numberForSingleThread));
                    });
                    // System.Console.WriteLine(intsLeft);
                }   
            }
        }

        private string GetSentence(int numberOfWords)
        {
            TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
            var random = new Random();
            int wordsToGenerate = numberOfWords - 2;
            var builder = new StringBuilder();
            builder.Append(myTI.ToTitleCase(GetRandomWord(words, random)));
            double decision;

            for (int i = 0; i < numberOfWords; i++)
            {
                decision = random.NextDouble();
                if(decision < 0.15)
                {
                    builder.Append(" ");
                    builder.Append(GetRandomDigit(random));
                }
                else if(decision < 0.25)
                {
                    builder.Append(GetRandomSpecialChar(random));
                }
                builder.Append(" ");
                builder.Append(GetRandomWord(words, random));
            }

            builder.Append(GetRandomWord(words, random));
            builder.Append(". ");
            decision = random.NextDouble();
            if(decision < 0.2)
            {
                builder.AppendLine();
            }
            return builder.ToString();
        }

        private string GetRandomWord(HashSet<string> words, Random random)
        {
            return words.ElementAt(random.Next(words.Count));
        }

        private string GetRandomSpecialChar(Random random)
        {
            return SpecialChars.ElementAt(random.Next(SpecialChars.Length)).ToString();
        }

        private string GetRandomDigit(Random random)
        {
            return Digits.ElementAt(random.Next(Digits.Length)).ToString();
        }
    }
}