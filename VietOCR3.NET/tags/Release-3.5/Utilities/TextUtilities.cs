/**
 * Copyright @ 2008 Quan Nguyen
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;

using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace Net.SourceForge.Vietpad.Utilities
{
    class TextUtilities
    {
        /// <summary>
        /// Changes letter case.
        /// </summary>
        /// <param name="typeOfCase"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ChangeCase(string text, string typeOfCase)
        {
            string result;

            if (typeOfCase == "UPPERCASE")
            {
                result = text.ToUpper();
            }
            else if (typeOfCase == "lowercase")
            {
                result = text.ToLower();
            }
            else if (typeOfCase == "Title_Case")
            {
                StringBuilder strB = new StringBuilder(text.ToLower());

                Regex regex = new Regex("(?<!\\p{IsCombiningDiacriticalMarks}|\\p{L})\\p{L}");      // word boundary \\b\\w
                MatchCollection mc = regex.Matches(text);

                // Loop through  the match collection to retrieve all 
                // matches and positions.
                for (int i = 0; i < mc.Count; i++)
                {
                    int index = mc[i].Index;
                    strB[index] = Char.ToUpper(strB[index]);
                }

                result = strB.ToString();
            }
            else if (typeOfCase == "Sentence_case")
            {
                StringBuilder strB = new StringBuilder(text.ToUpper() == text ? text.ToLower() : text);
                Regex regex = new Regex("\\p{L}(\\p{L}+)");
                MatchCollection mc = regex.Matches(text);

                for (Match m = regex.Match(text); m.Success; m = m.NextMatch())
                {
                    if (!(
                        m.Groups[0].Value.ToUpper() == m.Groups[0].Value ||
                        m.Groups[1].Value.ToLower() == m.Groups[1].Value
                        ))
                    {
                        for (int i = 0; i < mc.Count; i++)
                        {
                            int j = mc[i].Index;
                            strB[j] = Char.ToLower(strB[j]);
                        }
                    }
                }

                const string QUOTE = "\"'`,<>\u00AB\u00BB\u2018-\u203A";
                regex = new Regex("(?:[.?!\u203C-\u2049][])}"
                    + QUOTE + "]*|^|\n|:\\s+["
                    + QUOTE + "])[-=_*\u2010-\u2015\\s]*["
                    + QUOTE + "\\[({]*\\p{L}"
                    ); // begin of a sentence                  

                // Use the Matches method to find all matches in the input string.
                mc = regex.Matches(text);
                // Loop through  the match collection to retrieve all 
                // matches and positions.
                for (int i = 0; i < mc.Count; i++)
                {
                    int j = mc[i].Index + mc[i].Length - 1;
                    strB[j] = Char.ToUpper(strB[j]);
                }

                result = strB.ToString();
            }
            else
            {
                result = text;
            }

            return result;
        }

        /// <summary>
        /// Removes line breaks.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveLineBreaks(string text)
        {
            return Regex.Replace(
                    Regex.Replace(text.Replace(Environment.NewLine, "\n"),
                    "(?<=\n|^)[\t ]+|[\t ]+(?=$|\n)", string.Empty),
                    "(?<=.)\n(?=.)", " ").Replace("\n", Environment.NewLine);
        }
    }
}