/**
 * Copyright @ 2012 Quan Nguyen
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
using VietOCR.NET.Utilities;
using System.Threading;
using System.IO;
using VietOCR.NET.Postprocessing;
using System.Globalization;

namespace VietOCR.NET
{
    public partial class GUIWithBatch : VietOCR.NET.GUIWithSettings
    {
        private Queue<String> queue;
        private Watcher watcher;

        private StatusForm statusForm;

        delegate void UpdateStatusEvent(string message);

        public GUIWithBatch()
        {
            InitializeComponent();
            statusForm = new StatusForm();
            statusForm.Text = Properties.Resources.BatchProcessStatus;
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            queue = new Queue<String>();
            watcher = new Watcher(queue, watchFolder);
            watcher.Enabled = watchEnabled;

            System.Windows.Forms.Timer aTimer = new System.Windows.Forms.Timer();
            aTimer.Interval = 10000;
            aTimer.Tick += new EventHandler(OnTimedEvent);
            aTimer.Start();
        }

        private void OnTimedEvent(Object sender, EventArgs e)
        {
            if (queue.Count > 0)
            {
                if (this.statusForm.IsDisposed)
                {
                    this.statusForm = new StatusForm();
                    statusForm.Text = Properties.Resources.BatchProcessStatus;
                }
                if (!this.statusForm.Visible)
                {
                    this.statusForm.Show();
                }

                Thread t = new Thread(new ThreadStart(AutoOCR));
                t.Start();
            }
        }

        private void AutoOCR()
        {
            FileInfo imageFile;
            try
            {
                imageFile = new FileInfo(queue.Dequeue());
            }
            catch
            {
                return;
            }

            this.statusForm.TextBox.BeginInvoke(new UpdateStatusEvent(this.WorkerUpdate), new Object[] { imageFile.FullName });

            if (curLangCode == null)
            {
                this.statusForm.TextBox.BeginInvoke(new UpdateStatusEvent(this.WorkerUpdate), new Object[] { "    **  " + Properties.Resources.selectLanguage + "  **" });
                //queue.Clear();
                return;
            }

            IList<Image> imageList = ImageIOHelper.GetImageList(imageFile);
            if (imageList == null)
            {
                this.statusForm.TextBox.BeginInvoke(new UpdateStatusEvent(this.WorkerUpdate), new Object[] { "    **  " + Properties.Resources.Cannotprocess + imageFile.Name + "  **" });
                return;
            }

            try
            {
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = selectedPSM;
                string result = ocrEngine.RecognizeText(imageList, curLangCode);

                // postprocess to correct common OCR errors
                result = Processor.PostProcess(result, curLangCode);
                // correct common errors caused by OCR
                result = TextUtilities.CorrectOCRErrors(result);
                // correct letter cases
                result = TextUtilities.CorrectLetterCases(result);

                using (StreamWriter sw = new StreamWriter(Path.Combine(outputFolder, imageFile.Name + ".txt"), false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(result);
                }
            }
            catch (Exception e)
            {
                // Sets the UI culture to the selected language.
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedUILanguage);

                this.statusForm.TextBox.BeginInvoke(new UpdateStatusEvent(this.WorkerUpdate), new Object[] { "    **  " + Properties.Resources.Cannotprocess + imageFile.Name + "  **" });
                Console.WriteLine(e.StackTrace);
            }
        }

        void WorkerUpdate(string message)
        {
            this.statusForm.TextBox.AppendText(message + Environment.NewLine);
        }

        protected override void updateWatch()
        {
            watcher.Path = watchFolder;
            watcher.Enabled = watchEnabled;
        }
    }
}
