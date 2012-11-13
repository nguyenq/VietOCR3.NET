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
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithBatch : VietOCR.NET.GUIWithSettings
    {
        const string strInputFolder = "InputFolder";
        const string strBulkOutputFolder = "BulkOutputFolder";

        private string inputFolder;
        private string bulkOutputFolder;

        private BulkDialog bulkDialog;
        private Queue<String> queue;
        private Watcher watcher;

        private StatusForm statusForm;

        delegate void UpdateStatusEvent(string fileName);

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

        void WorkerUpdate(string fileName)
        {
            this.statusForm.TextBox.AppendText(fileName + Environment.NewLine);
        }

        protected override void updateWatch()
        {
            watcher.Path = watchFolder;
            watcher.Enabled = watchEnabled;
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        protected override void ChangeUILanguage(string locale)
        {
            base.ChangeUILanguage(locale);

            statusForm.Text = Properties.Resources.BatchProcessStatus;
        }
        protected override void bulkOCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bulkDialog == null)
            {
                bulkDialog = new BulkDialog();
            }

            bulkDialog.InputFolder = inputFolder;
            bulkDialog.OutputFolder = bulkOutputFolder;

            if (bulkDialog.ShowDialog() == DialogResult.OK)
            {
                inputFolder = bulkDialog.InputFolder;
                bulkOutputFolder = bulkDialog.OutputFolder;
            }
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            inputFolder = (string)regkey.GetValue(strInputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            bulkOutputFolder = (string)regkey.GetValue(strBulkOutputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strInputFolder, inputFolder);
            regkey.SetValue(strBulkOutputFolder, bulkOutputFolder);
        }
    }
}
