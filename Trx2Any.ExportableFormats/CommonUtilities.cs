
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Trx2Any.Common.TestBase;

namespace Trx2Any.ExportableFormats.Library
{
    internal static class CommonUtilities
    {
        public static DataTable CreateDataTable(UnitTestResultCollection collection,bool isIncludeOutput)
        {
            var dt = new DataTable();

            dt.Columns.Add("TestName");
            dt.Columns.Add("TestCategory");
            dt.Columns.Add("TestStatus");
            dt.Columns.Add("TestRun");
            dt.Columns.Add("TestCaseDescription");
            if (isIncludeOutput)
            {
                dt.Columns.Add("ErrorInformation");
                dt.Columns.Add("StandardOutput");
            }

            foreach (var t in collection)
            {
                var dr = dt.NewRow();
                dr["TestName"] = t.TestName;
                dr["TestCategory"] = t.TestCaseDescription;
                dr["TestStatus"] = t.TestStatus;
                dr["TestRun"] = t.TestRun;
                dr["TestCaseDescription"] = t.TestCaseDescription;
                if (isIncludeOutput)
                {
                    dr["ErrorInformation"] = t.ErrorInformation;
                    dr["StandardOutput"] = t.StandardOutput;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
