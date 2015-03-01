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
using System.Diagnostics;
using Tesseract;

namespace VietOCR.NET
{
    class OCRFiles : OCR<string>
    {
        /// <summary>
        /// Recognizes TIFF files.
        /// </summary>
        /// <param name="tiffFiles"></param>
        /// <param name="inputName">input filename; not used</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override string RecognizeText(IList<string> tiffFiles, string inputName)
        {
            string tempTessOutputFile = Path.GetTempFileName();
            File.Delete(tempTessOutputFile);
            tempTessOutputFile = Path.ChangeExtension(tempTessOutputFile, OutputFormat);
            string outputFileName = Path.Combine(Path.GetDirectoryName(tempTessOutputFile), Path.GetFileNameWithoutExtension(tempTessOutputFile)); // chop the file extension
            int psm = (int)Enum.Parse(typeof(PageSegMode), PageSegMode);

            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "tesseract.exe";

            StringBuilder result = new StringBuilder();

            foreach (string tiffFile in tiffFiles)
            {
                p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" -l {2} -psm {3} {4} {5} {6}", tiffFile, outputFileName, Language, psm, ControlParameters(), CONFIGS_FILE, OutputFormat == "hocr" ? "hocr" : OutputFormat == "pdf" ? "pdf" : string.Empty);
                p.Start();

                // Read the output stream first and then wait.
                string output = p.StandardOutput.ReadToEnd(); // ignore standard output
                string error = p.StandardError.ReadToEnd(); // 

                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    using (StreamReader sr = new StreamReader(tempTessOutputFile, Encoding.UTF8, true))
                    {
                        result.Append(sr.ReadToEnd());
                    }
                }
                else
                {
                    File.Delete(tempTessOutputFile);
                    if (error.Trim().Length == 0)
                    {
                        error = "Errors occurred.";
                    }
                    throw new ApplicationException(error);
                }
            }

            File.Delete(tempTessOutputFile);
            return result.ToString().Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Reads tessdata/configs/tess_configvars and SetVariable on Tesseract engine.
        /// This only works for non-init parameters (@see <a href="https://code.google.com/p/tesseract-ocr/wiki/ControlParams">ControlParams</a>).
        /// </summary>
        /// <param name="engine"></param>
        string ControlParameters()
        {
            string configsFilePath = Path.Combine(Datapath, "tessdata/configs/" + CONFIGVARS_FILE);
            if (!File.Exists(configsFilePath))
            {
                return string.Empty;
            }

            StringBuilder configvars = new StringBuilder();

            string[] lines = File.ReadAllLines(configsFilePath);
            foreach (string line in lines)
            {
                if (!line.Trim().StartsWith("#"))
                {
                    try
                    {
                        string[] keyValuePair = line.Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        configvars.AppendFormat("-c {0}={1} ", keyValuePair[0], keyValuePair[1]);
                    }
                    catch
                    {
                        //ignore and continue on
                    }
                }
            }

            return configvars.ToString();
        }
    }
}
