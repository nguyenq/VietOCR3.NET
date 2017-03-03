/**
 * Copyright @ 2017 Quan Nguyen
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
    public partial class GUIWithOEM : VietOCR.NET.GUIWithPSM
    {
        const string strOEM = "OcrEngineMode";
        ToolStripMenuItem oemItemChecked;

        public GUIWithOEM()
        {
            Dictionary<string, string> oemDict = new Dictionary<string, string>();
            oemDict.Add("TesseractOnly", "0 - Tesseract only");
            oemDict.Add("CubeOnly", "1 - Cube only");
            oemDict.Add("TesseractAndCube", "2 - Tesseract && Cube");
            oemDict.Add("Default", "3 - Default");

            InitializeComponent();

            //
            // Settings OCR Engine Mode submenu
            //
            EventHandler eh = new EventHandler(MenuOEMOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string mode in Enum.GetNames(typeof(EngineMode)))
            {
                ToolStripRadioButtonMenuItem oemItem = new ToolStripRadioButtonMenuItem();
                oemItem.Text = oemDict[mode];
                oemItem.Tag = mode;
                oemItem.CheckOnClick = true;
                oemItem.Click += eh;
                ar.Add(oemItem);
            }

            this.oemToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.oemToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.oemToolStripMenuItem.DropDownItems[i].Tag.ToString() == selectedOEM)
                {
                    // Select OEM last saved
                    oemItemChecked = (ToolStripMenuItem)oemToolStripMenuItem.DropDownItems[i];
                    oemItemChecked.Checked = true;
                    break;
                }
            }

            this.toolStripStatusLabelOEMvalue.Text = selectedOEM;
        }

        void MenuOEMOnClick(object obj, EventArgs ea)
        {
            if (oemItemChecked != null)
            {
                oemItemChecked.Checked = false;
            }
            oemItemChecked = (ToolStripMenuItem)obj;
            oemItemChecked.Checked = true;
            selectedOEM = oemItemChecked.Tag.ToString();
            this.toolStripStatusLabelOEMvalue.Text = selectedOEM;
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedOEM = (string)regkey.GetValue(strOEM, Enum.GetName(typeof(EngineMode), Tesseract.EngineMode.Default));
            try
            {
                // validate OEM value
                Tesseract.EngineMode oem = (EngineMode)Enum.Parse(typeof(EngineMode), selectedOEM);
            }
            catch 
            {
                selectedOEM = Enum.GetName(typeof(EngineMode), Tesseract.EngineMode.Default);
            }
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strOEM, selectedOEM);
        }
    }
}
