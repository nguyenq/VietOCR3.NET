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
using System.Xml;

namespace VietOCR.NET.Utilities
{
    class Utilities
    {
        /// <summary>
        /// Populates a dictionary with entries from an XML document.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="xmlFilePath"></param>
        public static void LoadFromXML(Dictionary<string, string> table, string xmlFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNodeList list = doc.GetElementsByTagName("entry");
            foreach (XmlNode node in list)
            {
                table.Add(node.Attributes[0].Value, node.InnerText);
            }
        }
    }
}
