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
using System.Globalization;
using Vietpad.NET.Controls;

namespace VietOCR.NET
{
    public partial class GUIWithUILanguage : VietOCR.NET.GUIWithInputMethod
    {
        ToolStripMenuItem miuilChecked;

        public GUIWithUILanguage()
        {
            InitializeComponent();

            //
            // Settings UI Language submenu
            //
            EventHandler eh = new EventHandler(MenuKeyboardUILangOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            String[] uiLangs = { "ca-ES", "cs-CZ", "en-US", "de-DE", "fa-IR", "hi-IN", "it-IT", "lt-LT", "ja-JP", "nl-NL", "pl-PL", "ru-RU", "sk-SK", "tr-TR", "vi-VN" }; // "bn-IN" caused exception on WinXP .NET 2.0
            foreach (string uiLang in uiLangs)
            {
                ToolStripRadioButtonMenuItem miuil = new ToolStripRadioButtonMenuItem();
                CultureInfo ci = new CultureInfo(uiLang);
                miuil.Tag = ci.Name;
                miuil.Text = ci.Parent.DisplayName + " (" + ci.Parent.NativeName + ")";
                if (ci.Parent.DisplayName.StartsWith("Invariant Language"))
                {
                    miuil.Text = ci.EnglishName.Substring(0, ci.EnglishName.IndexOf("(") - 1) + " (" + ci.NativeName.Substring(0, ci.NativeName.IndexOf("(") - 1) + ")";
                }
                miuil.CheckOnClick = true;
                miuil.Click += eh;
                ar.Add(miuil);
            }

            this.uiLanguageToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.uiLanguageToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.uiLanguageToolStripMenuItem.DropDownItems[i].Tag.ToString() == selectedUILanguage)
                {
                    // Select UI Language last saved
                    miuilChecked = (ToolStripMenuItem)uiLanguageToolStripMenuItem.DropDownItems[i];
                    miuilChecked.Checked = true;
                    break;
                }
            }
        }

        void MenuKeyboardUILangOnClick(object obj, EventArgs ea)
        {
            if (miuilChecked != null)
            {
                miuilChecked.Checked = false;
            }
            
            miuilChecked = (ToolStripMenuItem)obj;
            miuilChecked.Checked = true;
            if (selectedUILanguage != miuilChecked.Tag.ToString())
            {
                selectedUILanguage = miuilChecked.Tag.ToString();
                ChangeUILanguage(selectedUILanguage);
            }
        }
    }
}
