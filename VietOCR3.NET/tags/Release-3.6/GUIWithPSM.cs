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
using System.Windows.Forms;
using Vietpad.NET.Controls;
using Microsoft.Win32;
using Tesseract;

namespace VietOCR.NET
{
    public partial class GUIWithPSM : VietOCR.NET.GUIWithBatch
    {
        const string strPSM = "PageSegMode";
        ToolStripMenuItem psmItemChecked;

        public GUIWithPSM()
        {
            Dictionary<string, string> psmDict = new Dictionary<string, string>();
            psmDict.Add("OsdOnly", "0 - Orientation and script detection (OSD) only");
            psmDict.Add("AutoOsd", "1 - Automatic page segmentation with OSD");
            psmDict.Add("AutoOnly", "2 - Automatic page segmentation, but no OSD, or OCR");
            psmDict.Add("Auto", "3 - Fully automatic page segmentation, but no OSD (default)");
            psmDict.Add("SingleColumn", "4 - Assume a single column of text of variable sizes");
            psmDict.Add("SingleBlockVertText", "5 - Assume a single uniform block of vertically aligned text");
            psmDict.Add("SingleBlock", "6 - Assume a single uniform block of text");
            psmDict.Add("SingleLine", "7 - Treat the image as a single text line");
            psmDict.Add("SingleWord", "8 - Treat the image as a single word");
            psmDict.Add("CircleWord", "9 - Treat the image as a single word in a circle");
            psmDict.Add("SingleChar", "10 - Treat the image as a single character");
            psmDict.Add("Count", "11 - Number of enum entries");

            InitializeComponent();

            //
            // Settings PageSegMode submenu
            //
            EventHandler eh = new EventHandler(MenuPSMOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string mode in Enum.GetNames(typeof(PageSegMode)))
            {
                if ((PageSegMode)Enum.Parse(typeof(PageSegMode), mode) == PageSegMode.Count)
                {
                    continue;
                }
                ToolStripRadioButtonMenuItem psmItem = new ToolStripRadioButtonMenuItem();
                psmItem.Text = psmDict[mode];
                psmItem.Tag = mode;
                psmItem.CheckOnClick = true;
                psmItem.Click += eh;
                ar.Add(psmItem);
            }

            this.psmToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.psmToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.psmToolStripMenuItem.DropDownItems[i].Tag.ToString() == selectedPSM)
                {
                    // Select PSM last saved
                    psmItemChecked = (ToolStripMenuItem)psmToolStripMenuItem.DropDownItems[i];
                    psmItemChecked.Checked = true;
                    break;
                }
            }

            this.toolStripStatusLabelPSMvalue.Text = selectedPSM;
        }

        void MenuPSMOnClick(object obj, EventArgs ea)
        {
            if (psmItemChecked != null)
            {
                psmItemChecked.Checked = false;
            }
            psmItemChecked = (ToolStripMenuItem)obj;
            psmItemChecked.Checked = true;
            selectedPSM = psmItemChecked.Tag.ToString();
            this.toolStripStatusLabelPSMvalue.Text = selectedPSM;
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedPSM = (string)regkey.GetValue(strPSM, Enum.GetName(typeof(PageSegMode), Tesseract.PageSegMode.Auto));
            try
            {
                // validate PSM value
                Tesseract.PageSegMode psm = (PageSegMode)Enum.Parse(typeof(PageSegMode), selectedPSM);
            }
            catch 
            {
                selectedPSM = Enum.GetName(typeof(PageSegMode), Tesseract.PageSegMode.Auto);
            }
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strPSM, selectedPSM);
        }
    }
}
