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

namespace VietOCR.NET
{
    public partial class GUIWithSettings : VietOCR.NET.GUIWithUILanguage
    {
        const string strWatchEnable = "WatchEnable";
        const string strWatchFolder = "WatchFolder";
        const string strOutputFolder = "OutputFolder";
        const string strBatchOutputFormat = "BatchOutputFormat";

        protected string watchFolder;
        protected string outputFolder;
        protected bool watchEnabled;
        protected string outputFormat;

        private OptionsDialog optionsDialog;
 
        public GUIWithSettings()
        {
            InitializeComponent();
        }
        
        protected override void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionsDialog == null)
            {
                optionsDialog = new OptionsDialog();
            }

            optionsDialog.WatchFolder = watchFolder;
            optionsDialog.OutputFolder = outputFolder;
            optionsDialog.WatchEnabled = watchEnabled;
            optionsDialog.DangAmbigsPath = dangAmbigsPath;
            optionsDialog.DangAmbigsEnabled = dangAmbigsOn;
            optionsDialog.CurLangCode = curLangCode;
            optionsDialog.OutputFormat = outputFormat;

            if (optionsDialog.ShowDialog() == DialogResult.OK)
            {
                watchFolder = optionsDialog.WatchFolder;
                outputFolder = optionsDialog.OutputFolder;
                watchEnabled = optionsDialog.WatchEnabled;
                dangAmbigsPath = optionsDialog.DangAmbigsPath;
                dangAmbigsOn = optionsDialog.DangAmbigsEnabled;
                curLangCode = optionsDialog.CurLangCode;
                outputFormat = optionsDialog.OutputFormat;

                updateWatch();
            }
        }

        protected virtual void updateWatch()
        {
        }

        protected override void downloadLangDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadDialog downloadDialog = new DownloadDialog();
            downloadDialog.Owner = this;
            downloadDialog.LookupISO639 = LookupISO639;
            downloadDialog.LookupISO_3_1_Codes = LookupISO_3_1_Codes;
            downloadDialog.InstalledLanguages = InstalledLanguages;
            downloadDialog.ShowDialog();
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        protected override void ChangeUILanguage(string locale)
        {
            base.ChangeUILanguage(locale);

            if (optionsDialog != null)
            {
                optionsDialog.ChangeUILanguage(locale);
            }
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            watchEnabled = Convert.ToBoolean((int)regkey.GetValue(strWatchEnable, Convert.ToInt32(false)));
            watchFolder = (string)regkey.GetValue(strWatchFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            outputFolder = (string)regkey.GetValue(strOutputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            outputFormat = (string)regkey.GetValue(strBatchOutputFormat, "text");
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strWatchEnable, Convert.ToInt32(watchEnabled));
            regkey.SetValue(strWatchFolder, watchFolder);
            regkey.SetValue(strOutputFolder, outputFolder);
            regkey.SetValue(strBatchOutputFormat, outputFormat);
        }
    }
}
