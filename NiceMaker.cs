using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace TextGenerator
{
    public static class NiceMaker
    {
        public static void MakeNice(string filepath)
        {
            var lines = new List<string>();
            TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
            string line;
            FileStream fileStream = new FileStream(filepath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while(!reader.EndOfStream)
                {
                    line = reader.ReadLine().Trim();
                    var words = line.Split();
                    var builder = new StringBuilder();
                    bool firstDone = false;
                    for (int i = 0; i < words.Length; i++)
                    {
                        if(!firstDone)
                        {
                            if(words[i].Length > 1)
                            {
                                builder.Append(myTI.ToTitleCase(words[i]));
                                firstDone = true;
                                continue;
                            }
                            continue;
                        }
                        builder.Append($" {words[i]}");
                    }
                    lines.Add(builder.ToString());
                }
            }
        
            fileStream = new FileStream($"nice{filepath}", FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                foreach (var niceLine in lines)
                {
                    writer.WriteLine(niceLine);
                }
            }
        }
    }
}