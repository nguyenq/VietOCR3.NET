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
using Microsoft.Win32;
using System.Globalization;
using System.Threading;

using Net.SourceForge.Vietpad.InputMethod;

using Vietpad.NET.Controls;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class GUIWithInputMethod : VietOCR.NET.GUIWithFormat
    {
        ToolStripMenuItem miimChecked;

        private string selectedInputMethod;
        const string strInputMethod = "InputMethod";

        public GUIWithInputMethod()
        {
            InitializeComponent();

            //
            // Settings InputMethod submenu
            //
            EventHandler eh = new EventHandler(MenuKeyboardInputMethodOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string inputMethod in Enum.GetNames(typeof(InputMethods)))
            {
                ToolStripRadioButtonMenuItem miim = new ToolStripRadioButtonMenuItem();
                miim.Text = inputMethod;
                miim.CheckOnClick = true;
                miim.Click += eh;
                ar.Add(miim);
            }

            this.vietInputMethodToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
            this.textBox1.KeyPress += new KeyPressEventHandler(new VietKeyHandler(this.textBox1).OnKeyPress);
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.vietInputMethodToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.vietInputMethodToolStripMenuItem.DropDownItems[i].Text == selectedInputMethod)
                {
                    // Select InputMethod last saved
                    miimChecked = (ToolStripMenuItem)vietInputMethodToolStripMenuItem.DropDownItems[i];
                    miimChecked.Checked = true;
                    break;
                }
            }

            VietKeyHandler.InputMethod = (InputMethods)Enum.Parse(typeof(InputMethods), selectedInputMethod);
            VietKeyHandler.SmartMark = true;
            VietKeyHandler.ConsumeRepeatKey = true;
        }

        void MenuKeyboardInputMethodOnClick(object obj, EventArgs ea)
        {
            miimChecked.Checked = false;
            miimChecked = (ToolStripMenuItem)obj;
            miimChecked.Checked = true;
            selectedInputMethod = miimChecked.Text;
            VietKeyHandler.InputMethod = (InputMethods)Enum.Parse(typeof(InputMethods), selectedInputMethod);
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedInputMethod = (string)regkey.GetValue(strInputMethod, Enum.GetName(typeof(InputMethods), InputMethods.Telex));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strInputMethod, selectedInputMethod);
        }
        
    }
}

