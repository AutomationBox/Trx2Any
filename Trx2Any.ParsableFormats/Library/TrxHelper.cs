using System.Collections.Generic;
using System.Xml;

namespace Trx2Any.ParsableFormats.Library
{
    internal static class TrxHelper
    {
        public static Dictionary<string, string> FindTestIdAndTestName(string xml)
        {


            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var xmlnode = doc.GetElementsByTagName("TestDefinitions");



            var dictionary = new Dictionary<string, string>();


            foreach (XmlNode item in xmlnode)
            {
                var nodelist = item.ChildNodes;

                foreach (XmlNode itemList in nodelist)
                {
                    var xmlattrc = itemList.Attributes;
                    if (xmlattrc != null && dictionary.ContainsKey(xmlattrc["name"].Value))
                    {
                        dictionary.Add(xmlattrc["name"].Value + "_1", itemList.ChildNodes[0].InnerText);
                    }
                    else if (xmlattrc != null) dictionary.Add(xmlattrc["name"].Value, itemList.ChildNodes[0].InnerText);
                }

            }

            return dictionary;
        }

    }
}
