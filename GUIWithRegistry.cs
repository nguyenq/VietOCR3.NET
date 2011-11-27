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

namespace VietOCR.NET
{
    public partial class GUIWithRegistry : Form
    {
        const string strWinState = "WindowState";
        const string strLocationX = "LocationX";
        const string strLocationY = "LocationY";
        const string strWidth = "Width";
        const string strHeight = "Height";

        protected string strRegKey = "Software\\VietUnicode\\";

        Rectangle rectNormal;

        public GUIWithRegistry()
        {
            InitializeComponent();
            rectNormal = DesktopBounds;
        }

        protected override void OnMove(EventArgs ea)
        {
            base.OnMove(ea);

            if (WindowState == FormWindowState.Normal)
                rectNormal = DesktopBounds;
        }
        protected override void OnResize(EventArgs ea)
        {
            base.OnResize(ea);

            if (WindowState == FormWindowState.Normal)
                rectNormal = DesktopBounds;
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            // Load registry information.

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            LoadRegistryInfo(regkey);
            regkey.Close();
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            // Save registry information.

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey, true);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            SaveRegistryInfo(regkey);
            regkey.Close();
        }
        protected virtual void SaveRegistryInfo(RegistryKey regkey)
        {
            regkey.SetValue(strWinState, (int)WindowState);
            regkey.SetValue(strLocationX, rectNormal.X);
            regkey.SetValue(strLocationY, rectNormal.Y);
            regkey.SetValue(strWidth, rectNormal.Width);
            regkey.SetValue(strHeight, rectNormal.Height);
        }

        protected virtual void LoadRegistryInfo(RegistryKey regkey)
        {
            int x = (int)regkey.GetValue(strLocationX, 100);
            int y = (int)regkey.GetValue(strLocationY, 100);
            int cx = (int)regkey.GetValue(strWidth, 324);
            int cy = (int)regkey.GetValue(strHeight, 300);

            rectNormal = new Rectangle(x, y, cx, cy);

            // Adjust rectangle for any change in desktop size.

            Rectangle rectDesk = SystemInformation.WorkingArea;

            rectNormal.Width = Math.Min(rectNormal.Width, rectDesk.Width);
            rectNormal.Height = Math.Min(rectNormal.Height, rectDesk.Height);
            rectNormal.X -= Math.Max(rectNormal.Right - rectDesk.Right, 0);
            rectNormal.Y -= Math.Max(rectNormal.Bottom - rectDesk.Bottom, 0);

            // Set form properties.

            DesktopBounds = rectNormal;
            WindowState = (FormWindowState)regkey.GetValue(strWinState, 0);
        }
    }
}

