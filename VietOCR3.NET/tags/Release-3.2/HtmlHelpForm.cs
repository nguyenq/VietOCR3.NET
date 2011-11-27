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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace VietOCR.NET
{
    public partial class HtmlHelpForm : Form
    {
        const string ABOUT = "about:";

        public HtmlHelpForm(string helpFileName, string title)
        {
            InitializeComponent();
            this.Text = title;

            //foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            //    System.Console.WriteLine(name);

            this.webBrowser1.DocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET." + helpFileName);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            
            if (url.StartsWith(ABOUT) && url != "about:blank")
            {
                this.webBrowser1.DocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET." + url.Substring(ABOUT.Length));
            }
            else if (url.StartsWith("http"))
            {
                // Display external links using default webbrowser
                e.Cancel = true;
                System.Diagnostics.Process.Start(url);
            }
        }

        private void webBrowser1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.toolStripStatusLabel1.Text = this.webBrowser1.StatusText;
        }
    }
}