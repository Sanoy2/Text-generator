using System;

namespace TextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = args[0];
            string output = args[1];
            int numberOfWords = Int32.Parse(args[2]);
            int numberOfThreads = Int32.Parse(args[3]);
            System.Console.WriteLine(output);
            var words = Reader.GetWords(filepath);

            // Generator.Generate(words, output, numberOfWords);

            var newGenerator = new NewGenerator(words);
            newGenerator.Generate(output, numberOfWords);

            System.Console.WriteLine("out ready");
            // NiceMaker.MakeNice(output);
            // System.Console.WriteLine("nice ready");
        }
    }
}
