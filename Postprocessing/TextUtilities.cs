using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace VietOCR.NET.Postprocessing
{
    class TextUtilities
    {
        private static List<Dictionary<string, string>> maps;
        private static DateTime mapLastModified = DateTime.MinValue;

        /// <summary>
        /// Corrects letter cases.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CorrectLetterCases(String input)
        {
            // lower uppercase letters ended by lowercase letters except the first letter
            Regex regex = new Regex("(?<=\\p{L}+)(\\p{Lu}+)(?=\\p{Ll}+)");
            input = regex.Replace(input, new MatchEvaluator(LowerCaseText));

            //// lower uppercase letters begun by lowercase letters
            regex = new Regex("(?<=\\p{Ll}+)(\\p{Lu}+)");
            input = regex.Replace(input, new MatchEvaluator(LowerCaseText));

            return input;
        }

        static string LowerCaseText(Match m)
        {
            // Lowercase the matched string.
            return m.Value.ToLower();
        }

        public static List<Dictionary<string, string>> LoadMap(string dangAmbigsFile)
        {
            try
            {
                FileInfo dataFile = new FileInfo(dangAmbigsFile);

                DateTime fileLastModified = dataFile.LastWriteTime;
                if (maps == null)
                {
                    maps = new List<Dictionary<string, string>>();
                }
                else
                {
                    if (fileLastModified <= mapLastModified)
                    {
                        return maps; // no need to reload map
                    }
                    maps.Clear();
                }
                mapLastModified = fileLastModified;

                for (int i = Processor.PLAIN; i <= Processor.REGEX; i++)
                {
                    maps.Add(new Dictionary<string, string>());
                }

                StreamReader sr = new StreamReader(dangAmbigsFile, Encoding.UTF8);
                string str;

                while ((str = sr.ReadLine()) != null)
                {
                    // skip empty line or line starts with # or without tab delimiters
                    if (str.Trim().Length == 0 || str.Trim().StartsWith("#") || !str.Contains("\t"))
                    {
                        continue;
                    }

                    str = Regex.Replace(str, "\t+", "\t");
                    string[] parts = str.Split('\t');
                    if (parts.Length < 3)
                    {
                        continue;
                    }

                    int type = int.Parse(parts[0]);
                    string key = parts[1];
                    string value = parts[2];

                    if (type < Processor.PLAIN || type > Processor.REGEX)
                    {
                        continue;
                    }

                    Dictionary<string, string> dict = maps[type];
                    dict[key] = value;
                }
                sr.Close();   
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return maps;
        }
    }
}
