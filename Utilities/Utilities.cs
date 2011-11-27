using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VietOCR.NET.Utilities
{
    class Utilities
    {
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
