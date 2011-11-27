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
namespace VietOCR.NET.Postprocessing
{
    using System;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;

    public class Processor
    {
        public static string PostProcess(string text, string langCode)
        {
            try
            {
                IPostProcessor processor = ProcessorFactory.createProcessor((ISO639)Enum.Parse(typeof(ISO639), langCode.Substring(0, 3)));
                return processor.PostProcess(text);
            }
            catch
            {
                return text;
            }
        }

        public static string PostProcess(string text, string langCode, string dangAmbigsPath, bool dangAmbigsOn)
        {
            if (text.Trim().Length == 0)
            {
                return text;
            }

            // correct using external x.DangAmbigs.txt file first, if enabled
            if (dangAmbigsOn)
            {
                StringBuilder strB = new StringBuilder(text);

                // replace text based on entries read from an x.DangAmbigs.txt file
                Dictionary<string, string> replaceRules = TextUtilities.LoadMap(Path.Combine(dangAmbigsPath, langCode + ".DangAmbigs.txt"));
                if (replaceRules.Count == 0 && langCode.Length > 3)
                {
                    replaceRules = TextUtilities.LoadMap(Path.Combine(dangAmbigsPath, langCode.Substring(0, 3) + ".DangAmbigs.txt")); // fall back on base
                }

                if (replaceRules.Count == 0)
                {
                    throw new NotSupportedException(langCode);
                }

                Dictionary<string, string>.KeyCollection.Enumerator enumer = replaceRules.Keys.GetEnumerator();

                while (enumer.MoveNext())
                {
                    string key = enumer.Current;
                    string value = replaceRules[key];
                    strB = strB.Replace(key, value);
                }
                text = strB.ToString();
            }

            // postprocessor
            text = PostProcess(text, langCode);

            // correct common errors caused by OCR
            text = TextUtilities.CorrectOCRErrors(text);

            // correct letter cases
            return TextUtilities.CorrectLetterCases(text);
        }
    }
}