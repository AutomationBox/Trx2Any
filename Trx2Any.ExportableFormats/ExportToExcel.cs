using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using Trx2Any.Common.Interfaces;
using Trx2Any.Common.TestBase;

namespace Trx2Any.ExportableFormats
{
    [Export(typeof (IExportableFormat))]
    [ExportableFormatMetadata(FormatName = "excel2007")]
    public class ExportToExcel2007 : IExportableFormat
    {
        public bool ExportData(TestSummary testSummaryCollection,
                               UnitTestResultCollection unitTestResultCollection,
                               string fileName)
        {
            GenerateReport(unitTestResultCollection, testSummaryCollection, fileName);
            return true;
        }

        private static void GenerateReport(UnitTestResultCollection unitTestResultCollection,
                                           TestSummary testSummaryCollection, string fileName)
        {
            var file = new System.IO.FileInfo(fileName);
            if (file.Exists)
                file.Delete();
            using (var p = new ExcelPackage(file))
            {
                //set the workbook properties and add a default sheet in it
                SetWorkbookProperties(p);
                //Create a sheet
                ExcelWorksheet ws = CreateSheet(p, unitTestResultCollection.CollectionName);
                DataTable dt = CreateDataTable(unitTestResultCollection);

                ////Merging cells and create a center heading for out table
                ws.Cells[1, 1].Value = "Trx2Any - Trx to Excel Export";
                ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                int rowIndex = 2;

                //Create Summary Table
                CreateSummaryHeaders(ws, ref rowIndex, dt.Columns.Count, testSummaryCollection);

                CreateHeader(ws, ref rowIndex, dt);
                CreateData(ws, ref rowIndex, dt);

                ws.Column(dt.Columns.Count).Width = 100;
                p.Save();
            }
        }

        private static void CreateSummaryHeaders(ExcelWorksheet ws, ref int rowIndex, int testResultColumnCount,
                                                 TestSummary collection)
        {
            ws.Cells[rowIndex, 1].Value = "Summary";
            ws.Cells[rowIndex, 1, rowIndex, testResultColumnCount].Merge = true;
            ws.Cells[rowIndex, 1, rowIndex, testResultColumnCount].Style.Font.Bold = true;
            ws.Cells[rowIndex, 1, rowIndex, testResultColumnCount].Style.HorizontalAlignment =
                ExcelHorizontalAlignment.Center;
            rowIndex++;

            ws.Cells[rowIndex, 1].Value = "TotalTestCaseRun";
            ws.Cells[rowIndex, 2].Value = "Passed";
            ws.Cells[rowIndex, 3].Value = "Failed";
            ws.Cells[rowIndex, 4].Value = "Inconclusive";

            rowIndex++;

            ws.Cells[rowIndex, 1].Value = collection.TotalTestCaseRun;
            ws.Cells[rowIndex, 2].Value = collection.Passed;
            ws.Cells[rowIndex, 3].Value = collection.Failed;
            ws.Cells[rowIndex, 4].Value = collection.Inconclusive;

            rowIndex++;
        }

        /// <summary>
        /// Sets the workbook properties and adds a default sheet.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        private static void SetWorkbookProperties(ExcelPackage p)
        {
            //Here setting some document properties
            p.Workbook.Properties.Author = "trx2Any";
            p.Workbook.Properties.Title = "Trx2ExcelReports";
        }

        /// <summary>
        /// Creates the data table with some dummy data.
        /// </summary>
        /// <returns>DataTable</returns>
        private static DataTable CreateDataTable(UnitTestResultCollection collection)
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

        private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 1;

            ws.Cells[rowIndex, 1].Value = "Test Case Details";
            ws.Cells[rowIndex, 1, rowIndex, dt.Columns.Count].Merge = true;
            ws.Cells[rowIndex, 1, rowIndex, dt.Columns.Count].Style.Font.Bold = true;
            ws.Cells[rowIndex, 1, rowIndex, dt.Columns.Count].Style.HorizontalAlignment =
                ExcelHorizontalAlignment.Center;
            rowIndex++;

            foreach (DataColumn dc in dt.Columns) //Creating Headers
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting the background color of header cells to Gray
                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.Brown);

                ////Setting Top/left,right/bottom borders.
                //var border = cell.Style.Border;
                //border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value = dc.ColumnName;
                cell.Style.WrapText = true;

                colIndex++;
            }
        }

        private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                var colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //Setting Value in cell
                    cell.Value = (dr[dc.ColumnName]);
                    //cell.Style.ShrinkToFit = true;
                    cell.Style.WrapText = true;
                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    colIndex++;
                }
            }
        }

        private static void CreateFooter(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
        }

        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[sheetName];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }
    }
}