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
using OCR.TesseractWrapper;

namespace VietOCR.NET
{
    public partial class GUIWithPSM : VietOCR.NET.GUIWithSettings
    {
        const string strPSM = "PageSegMode";
        ToolStripMenuItem psmItemChecked;

        public GUIWithPSM()
        {
            Dictionary<string, string> psmDict = new Dictionary<string, string>();
            psmDict.Add("PSM_OSD_ONLY", "0 - Orientation and script detection (OSD) only");
            psmDict.Add("PSM_AUTO_OSD", "1 - Automatic page segmentation with OSD");
            psmDict.Add("PSM_AUTO_ONLY", "2 - Automatic page segmentation, but no OSD, or OCR");
            psmDict.Add("PSM_AUTO", "3 - Fully automatic page segmentation, but no OSD (default)");
            psmDict.Add("PSM_SINGLE_COLUMN", "4 - Assume a single column of text of variable sizes");
            psmDict.Add("PSM_SINGLE_BLOCK_VERT_TEXT", "5 - Assume a single uniform block of vertically aligned text");
            psmDict.Add("PSM_SINGLE_BLOCK", "6 - Assume a single uniform block of text");
            psmDict.Add("PSM_SINGLE_LINE", "7 - Treat the image as a single text line");
            psmDict.Add("PSM_SINGLE_WORD", "8 - Treat the image as a single word");
            psmDict.Add("PSM_CIRCLE_WORD", "9 - Treat the image as a single word in a circle");
            psmDict.Add("PSM_SINGLE_CHAR", "10 - Treat the image as a single character");
            psmDict.Add("PSM_COUNT", "11 - Number of enum entries");

            InitializeComponent();

            //
            // Settings PageSegMode submenu
            //
            EventHandler eh = new EventHandler(MenuPSMOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string mode in Enum.GetNames(typeof(ePageSegMode)))
            {
                if ((ePageSegMode)Enum.Parse(typeof(ePageSegMode), mode) == ePageSegMode.PSM_COUNT)
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
        }

        void MenuPSMOnClick(object obj, EventArgs ea)
        {
            psmItemChecked.Checked = false;
            psmItemChecked = (ToolStripMenuItem)obj;
            psmItemChecked.Checked = true;
            selectedPSM = psmItemChecked.Tag.ToString();
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedPSM = (string)regkey.GetValue(strPSM, Enum.GetName(typeof(ePageSegMode), ePageSegMode.PSM_AUTO));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strPSM, selectedPSM);
        }
    }
}
