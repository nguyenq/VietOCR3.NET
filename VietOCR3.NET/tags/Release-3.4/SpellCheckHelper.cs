using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NHunspell;
using System.Text.RegularExpressions;
using System.Collections;
using VietOCR.NET.Controls;
using Net.SourceForge.Vietpad.Utilities;

namespace VietOCR.NET
{
    class SpellCheckHelper
    {
        TextBoxBase textbox;
        string localeId;
        String baseDir;
        static ArrayList listeners = new ArrayList();
        static List<CharacterRange> spellingErrorRanges = new List<CharacterRange>();
        static List<String> userWordList = new List<String>();
        static DateTime mapLastModified = DateTime.MinValue;
        Hunspell spellChecker;
        CustomPaintTextBox cntl;

        public CharacterRange[] GetSpellingErrorRanges
        {
            get { return spellingErrorRanges.ToArray(); }
        }

        public SpellCheckHelper(TextBoxBase textbox, string localeId)
        {
            this.textbox = textbox;
            this.localeId = localeId;

            baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public void EnableSpellCheck()
        {
            if (localeId == null)
            {
                return;
            }
            try
            {
                string dictPath = baseDir + "/dict/" + localeId;
                spellChecker = new Hunspell(dictPath + ".aff", dictPath + ".dic");
                LoadUserDictionary();

                listeners.Add(new System.EventHandler(this.textbox_TextChanged));

                this.textbox.TextChanged += (System.EventHandler)listeners[0];
                cntl = new CustomPaintTextBox(this.textbox, this);

                SpellCheck();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DisableSpellCheck()
        {
            if (listeners.Count > 0)
            {
                this.textbox.TextChanged -= (System.EventHandler)listeners[0];
                listeners.RemoveAt(0);
            }
            spellingErrorRanges.Clear();
        }

        public void SpellCheck()
        {
            this.textbox.Invalidate();
            spellingErrorRanges.Clear();
            List<String> words = ParseText(textbox.Text);
            List<String> misspelledWords = SpellCheck(words);
            if (misspelledWords.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (String word in misspelledWords)
            {
                sb.Append(word).Append("|");
            }
            sb.Length -= 1; //remove last |

            // build regex
            String patternStr = "\\b(" + sb.ToString() + ")\\b";
            Regex regex = new Regex(patternStr, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(textbox.Text);

            // Loop through  the match collection to retrieve all 
            // matches and positions.
            for (int i = 0; i < mc.Count; i++)
            {
                spellingErrorRanges.Add(new CharacterRange(mc[i].Index, mc[i].Length));
            }
            
            //new CustomPaintTextBox(textbox, this);
        }

        List<String> SpellCheck(List<String> words)
        {
            List<String> misspelled = new List<String>();

            foreach (String word in words)
            {
                if (!spellChecker.Spell(word))
                {
                    // is mispelled word in user.dic?
                    if (!userWordList.Contains(word.ToLower()))
                    {
                        misspelled.Add(word);
                    }

                }
            }

            return misspelled;
        }

        List<String> ParseText(String text)
        {
            List<String> words = new List<String>();
            BreakIterator boundary = BreakIterator.GetWordInstance();
            boundary.Text = text;
            int start = boundary.First();
            for (int end = boundary.Next(); end != BreakIterator.DONE; start = end, end = boundary.Next())
            {
                if (!Char.IsLetter(text[start]))
                {
                    continue;
                }
                words.Add(text.Substring(start, end - start));
            }

            return words;
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            SpellCheck();
        }

        public List<String> Suggest(string misspelled)
        {
            List<String> list = new List<String>();
            list.Add(misspelled);

            if (SpellCheck(list).Count == 0)
            {
                return null;
            }
            else
            {
                return spellChecker.Suggest(misspelled); // TODO: exception thrown here.
            }
        }

        public void IgnoreWord(string word)
        {
            if (!userWordList.Contains(word.ToLower()))
            {
                userWordList.Add(word.ToLower());
            }
        }

        public void AddWord(string word)
        {
            if (!userWordList.Contains(word.ToLower()))
            {
                userWordList.Add(word.ToLower());
                String baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string strUserDictFile = Path.Combine(baseDir, @"dict\user.dic");

                using (StreamWriter sw = new StreamWriter(strUserDictFile, true, Encoding.UTF8))
                {
                    sw.WriteLine(word);
                    sw.Close();
                }
            }
        }
        void LoadUserDictionary()
        {
            try
            {
                String baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string strUserDictFile = Path.Combine(baseDir, @"dict\user.dic");
                FileInfo userDict = new FileInfo(strUserDictFile);
                DateTime fileLastModified = userDict.LastWriteTime;

                if (fileLastModified <= mapLastModified)
                {
                    return; // no need to reload dictionary
                }

                mapLastModified = fileLastModified;
                userWordList.Clear();

                using (StreamReader sr = new StreamReader(strUserDictFile, Encoding.UTF8))
                {
                    string str;

                    while ((str = sr.ReadLine()) != null)
                    {
                        userWordList.Add(str.ToLower());
                    }
                    //sr.Close();
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
