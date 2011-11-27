/*
* BreakIterator
*
* Attempt to mimic Java's BreakIterator class
* to analyze word boundaries.
*
* @author: Quan Nguyen
* @version: 1.4, 10 January 2011
*/

using System.Text.RegularExpressions;

namespace Net.SourceForge.Vietpad.Utilities
{
    public class BreakIterator
    {
        private static BreakIterator instance;

        private string text;
        static int index;
        static MatchCollection mc;
        public static readonly int DONE = -1;

        static readonly Regex regex = new Regex(@"\b.", RegexOptions.Compiled | RegexOptions.Singleline);

        private BreakIterator() { }

        public static BreakIterator GetWordInstance()
        {
            if (instance == null)
            {
                instance = new BreakIterator();
            }

            return instance;
        }

        public string Text
        {
            set
            {
                text = value;
                mc = regex.Matches(text); // collection of all word boundaries
                //		for (int i = 0; i < mc.Count; i++)
                //		{    
                //			System.Console.WriteLine("Found '{0}' at position {1}", mc[i].Value, mc[i].Index);
                //		}        
            }
        }
        public int First()
        {
            index = 0;
            if (mc.Count > 0)
            {
                return mc[0].Index;
            }
            else
            {
                return index;
            }
        }
        public int Last()
        {
            index = mc.Count;
            return text.Length;
        }

        public int Next()
        {
            while (index < mc.Count)
            {
                index++;
                if (index >= mc.Count)
                {
                    return text.Length;
                }
                else
                {
                    return mc[index].Index;
                }
            }

            index = mc.Count;
            return DONE;
        }
        public int Previous()
        {
            while (index > 0)
            {
                index--;
                if (index < 0)
                {
                    return 0;
                }
                else
                {
                    return mc[index].Index;
                }
            }

            index = 0;
            return DONE;
        }

        public int Following(int offset)
        {
            int start = First();
            for (int end = Next(); end != BreakIterator.DONE; start = end, end = Next())
            {
                if (end > offset)
                {
                    return end;
                }
            }
            return DONE;
        }

        public int Preceding(int offset)
        {
            int start = First();
            for (int end = Next(); end != BreakIterator.DONE; start = end, end = Next())
            {
                if (end > offset)
                {
                    return Previous();
                }
            }
            return DONE;
        }
    }
}