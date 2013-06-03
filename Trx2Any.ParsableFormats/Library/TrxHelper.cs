using System.Collections.Generic;
using System.Data;
using System.Xml;
using Trx2Any.Common.TestBase;

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

        public static DataTable CreateDataTable(UnitTestResultCollection collection)
        {
            var dt = new DataTable();

            dt.Columns.Add("TestName");
            dt.Columns.Add("TestCategory");
            dt.Columns.Add("TestStatus");
            dt.Columns.Add("TestRun");
            dt.Columns.Add("TestCaseDescription");
            dt.Columns.Add("ErrorInformation");
            dt.Columns.Add("StandardOutput");

            foreach (var t in collection)
            {
                var dr = dt.NewRow();
                dr["TestName"] = t.TestName;
                dr["TestCategory"] = t.TestCaseDescription;
                dr["TestStatus"] = t.TestStatus;
                dr["TestRun"] = t.TestRun;
                dr["TestCaseDescription"] = t.TestCaseDescription;
                dr["ErrorInformation"] = t.ErrorInformation;
                dr["StandardOutput"] = t.StandardOutput;

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
