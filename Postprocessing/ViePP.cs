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
    using System.Text.RegularExpressions;

    public class ViePP : IPostProcessor
    {
        const string TONE = "[\u0300\u0309\u0303\u0301\u0323]?"; // `?~'.
        const string DOT_BELOW = "\u0323?"; // .
        const string MARK = "[\u0306\u0302\u031B]?"; // (^+
        const string VOWEL = "[aeiouy]";

        public string PostProcess(string text)
        {
            // Move all of these String replace to external vie.DangAmbigs.txt.
            // The file location also gives users more control over the choice of word corrections.
            //// substitute Vietnamese letters frequently misrecognized by Tesseract 2.03
            //StringBuilder strB = new StringBuilder(text);
            //strB.Replace("êĩ-", "ết")
            //    .Replace("tmg", "úng")
            //    ;

            text =  Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(text,
                        "(?i)(?<=đ)ă\\b", "ã"),
                        "(?i)(?<=[ch])ă\\b", "ả"),
                        "(?i)ă(?![cmnpt])", "à"),
                        "(?i)ẵ(?=[cpt])", "ắ"),
                        "(?<=\\b[Tt])m", "rư"),
                        "(?i)\\bl(?=[rh])", "t"),
                        "(u|ll|r)(?=[gh])", "n"),
                        "(iii|ln|rn)", "m"),
                        "(?i)(?<=[mqrgsv])ll", "u"),
                        "(?i)(?<=[cknpt])ll", "h"),
                        "(?i)[oe](?=h)", "c"),
                        "\\Bđ", "ớ")
                    ;

            string nfdText = text.Normalize(NormalizationForm.FormD);
            nfdText = Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(nfdText,
                        "(?i)(?<![q])(u)(?=o\u031B" + TONE + "\\p{L})", "$1\u031B"), // uo+n to u+o+n 
                        "(?i)(?<=u\u031B)(o)(?=" + TONE + "\\p{L})", "$1\u031B"), // u+on to u+o+n
                        "(?i)(i)" + TONE + "(?=[eioy])", "$1"), // remove mark on i followed by certain vowels
                        "(?i)(?<=gi)" + TONE + "(?=[aeiouy])", ""), // remove mark on i preceeded by g and followed by any vowel
                        // It seems to be a bug with .NET: it should be \\b, not \\B,
                        // unless combining diacritical characters are not considered as words by .NET.
                        "(?i)(?<=[^q]" + VOWEL + "\\p{IsCombiningDiacriticalMarks}{0,2})(i)" + TONE + "\\B", "$1"), // remove mark on i preceeded by vowels w/ or w/o diacritics
                        "(?i)(?<=[aeo]\u0302)['\u2018\u2019]", "\u0301"), // ^right-single-quote to ^acute
                        "(?i)\u2018([aeo]\u0302)(?!\\p{IsCombiningDiacriticalMarks})", "$1\u0300"), // left-single-quote+a^ to a^grave
                        "(?i)(?<=[aeo]\u0302)h", "\u0301n"), // a^+h to a^acute+n
                        "(?i)(?<=[uo]" + TONE + ")['\u2018]", "\u031B"), // u'+left-single-quote) to u+' 
                        "(?i)(?<=" + VOWEL + "\\p{IsCombiningDiacriticalMarks}{0,2})l\\b", "t"), // vowel+diacritics+l to vowel+diacritics+t
                        "(?i)(?<=" + VOWEL + "\\p{IsCombiningDiacriticalMarks}{0,2})ll\\b", "u"), // vowel+diacritics+ll to vowel+diacritics+u
                        "\\B\\$(?="+ VOWEL + ")", "S") // replace leading $ followed by vowel with S
                    ;

            return nfdText.Normalize();
        }
    }
}