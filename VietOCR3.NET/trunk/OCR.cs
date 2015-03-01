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
using System.Text;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace VietOCR.NET
{
    abstract class OCR<T>
    {
        protected const string CONFIGS_FILE = "tess_configs";
        protected const string CONFIGVARS_FILE = "tess_configvars";

        protected readonly string Datapath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        protected Rectangle rect = Rectangle.Empty;
        BackgroundWorker worker;
        private string pageSegMode = "3"; // or alternatively, "Auto"; // 3 - Fully automatic page segmentation, but no OSD (default)

        public string PageSegMode
        {
            get { return pageSegMode; }
            set { pageSegMode = value; }
        }

        private string language = "eng";

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        private string outputFormat = "text";

        public string OutputFormat
        {
            get { return outputFormat; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    outputFormat = value;
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<T> imageEntities, string inputName, Rectangle selection)
        {
            rect = selection;
            return RecognizeText(imageEntities, inputName);
        }

        /// <summary>
        /// Recognize text.
        /// </summary>
        /// <param name="imageEntities"></param>
        /// <param name="index"></param>
        /// 
        /// <returns></returns>
        public abstract string RecognizeText(IList<T> imageEntities, string inputName);

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<T> imageEntities, string inputName, Rectangle selection, BackgroundWorker worker, DoWorkEventArgs e)
        {
            rect = selection;
            return RecognizeText(imageEntities, inputName, worker, e);
        }

        /// <summary>
        /// Recognize text.
        /// </summary>
        /// <param name="imageEntities">list of imageEntities</param>
        /// <param name="inputName">input filename</param>
        /// <param name="index">index of page (frame) of image; -1 for all</param>
        /// <param name="lang">the language OCR is going to be performed for</param>
        /// <returns>result text</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<T> imageEntities, string inputName, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Abort the operation if the user has canceled.
            // Note that a call to CancelAsync may have set 
            // CancellationPending to true just after the
            // last invocation of this method exits, so this 
            // code will not have the opportunity to set the 
            // DoWorkEventArgs.Cancel flag to true. This means
            // that RunWorkerCompletedEventArgs.Cancelled will
            // not be set to true in your RunWorkerCompleted
            // event handler. This is a race condition.
            this.worker = worker;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return String.Empty;
            }

            return RecognizeText(imageEntities, inputName);
        }

        void ProgressEvent(int percent)
        {
            worker.ReportProgress(percent);
        }
    }
}
