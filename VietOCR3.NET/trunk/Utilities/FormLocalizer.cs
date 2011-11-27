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
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;

namespace VietOCR.NET.Utilities
{
    class FormLocalizer
    {
        private Form form;
        private Type formType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="form"></param>
        public FormLocalizer(Form form, Type formType)
        {
            this.form = form;
            this.formType = formType;
        }

        /// <summary>
        /// Update UI elements.
        /// Original code from http://secure.codeproject.com/KB/locale/ChangeUICulture.aspx
        /// </summary>
        /// <param name="culture"></param>
        public void ApplyCulture(CultureInfo culture)
        {
            // Applies culture to current Thread.
            Thread.CurrentThread.CurrentUICulture = culture;

            // Create a resource manager for this Form
            // and determine its fields via reflection.

            ComponentResourceManager resources = new ComponentResourceManager(formType);
            FieldInfo[] fieldInfos = formType.GetFields(BindingFlags.Instance |
                BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

            // Call SuspendLayout for Form and all fields derived from Control, so assignment of 
            // localized text doesn't change layout immediately.

            form.SuspendLayout();
            // If available, assign localized text to Form and fields with Text property.

            String text = resources.GetString("$this.Text");
            if (text != null)
                form.Text = text;

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Type fieldType = fieldInfo.FieldType;
                if (fieldType.IsSubclassOf(typeof(Control)) || fieldType.IsSubclassOf(typeof(ToolStripItem)))
                {
                    if (fieldType.IsSubclassOf(typeof(Control)))
                    {
                        fieldType.InvokeMember("SuspendLayout",
                            BindingFlags.InvokeMethod, null,
                            fieldInfo.GetValue(form), null);
                    }

                    if (fieldType.GetProperty("Text", typeof(String)) != null)
                    {
                        text = resources.GetString(fieldInfo.Name + ".Text");
                        if (text != null)
                        {
                            fieldType.InvokeMember("Text",
                                BindingFlags.SetProperty, null,
                                fieldInfo.GetValue(form), new object[] { text });
                        }
                    }

                    if (fieldType.GetProperty("ToolTipText", typeof(String)) != null)
                    {
                        text = resources.GetString(fieldInfo.Name + ".ToolTipText");
                        if (text != null)
                        {
                            fieldType.InvokeMember("ToolTipText",
                                BindingFlags.SetProperty, null,
                                fieldInfo.GetValue(form), new object[] { text });
                        }
                    }

                    // Call ResumeLayout for Form and all fields
                    // derived from Control to resume layout logic.
                    // Call PerformLayout, so layout changes due
                    // to assignment of localized text are performed.
                    if (fieldType.IsSubclassOf(typeof(Control)))
                    {
                        fieldType.InvokeMember("ResumeLayout",
                                BindingFlags.InvokeMethod, null,
                                fieldInfo.GetValue(form), new object[] { false });
                    }
                }
            }

            form.ResumeLayout(false);
            form.PerformLayout();
        }
    }
}
