using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VietOCR.NET
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GUIWithTools());
            }
            else
            {
                // Command line given, display console
                if (!AttachConsole(-1)) // Attach to a parent process console
                {
                    AllocConsole(); // Alloc a new console
                }

                ConsoleApp.Main(args);
            }
        }
    }
}