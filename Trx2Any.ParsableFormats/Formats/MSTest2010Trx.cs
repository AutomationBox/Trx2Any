using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml;
using Trx2Any.Common.Interfaces;
using Trx2Any.Common.TestBase;
using Trx2Any.ParsableFormats.Library;

namespace Trx2Any.ParsableFormats.Formats
{
    [Export(typeof(ITrxParsable))]
    public sealed class MSTest2010Trx : ITrxParsable
    {
        const string BaseXPath = "/tns:TestRun/tns:Results/tns:TestResultAggregation/tns:InnerResults/tns:UnitTestResult";
        const string RegisteredNamespace = "tns";
        const string Test2010Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
        const string BaseXPathForOtherCases = "/tns:TestRun/tns:Results/tns:TestResultAggregation/tns:InnerResults/tns:TestResult";
        const string InnerResultNodeListXPath = "/tns:UnitTestResult/tns:InnerResults";
        const string InnerUnitTestResultXPath = "/tns:UnitTestResult/tns:InnerResults/tns:UnitTestResult";
        const string OutputXPath = "/tns:Output";
        const string StandardOutputXPath = "/tns:UnitTestResult/tns:Output/tns:StdOut";
        const string ErrorInfoXPath = "/tns:UnitTestResult/tns:Output/tns:ErrorInfo";

        readonly UnitTestResultCollection _unitTestResultCollection;
        Dictionary<string, string> _testIds = new Dictionary<string, string>();

        [ImportingConstructor]
        public MSTest2010Trx([Import("ParsedFilePath")]string trxPath)
        {
            var trxFileName = new FileInfo(trxPath);
            _unitTestResultCollection
                                        = new UnitTestResultCollection(trxFileName.Name);

            PrepareTestCollection(trxFileName);

            

        }

        private void PrepareTestCollection(FileInfo trxPath)
        {
            var sr = new StreamReader(trxPath.FullName);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sr.ReadToEnd());
            _testIds.Clear();
            _testIds = TrxHelper.FindTestIdAndTestName(xmlDoc.InnerXml);
            var nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace(RegisteredNamespace, Test2010Namespace);
            
            var basicLevelInnerResults = xmlDoc.SelectNodes(BaseXPath, nsMgr);
            if (basicLevelInnerResults != null)
                foreach (XmlNode selectedNode in basicLevelInnerResults)
                {
                    ReadUnitTestResultNodes(selectedNode, nsMgr);
                }

            //Adding support for test cases not executed
            var xmlNodeList = xmlDoc.SelectNodes(BaseXPathForOtherCases, nsMgr);
            if (xmlNodeList != null)
                foreach (XmlNode selectedNode in xmlNodeList)
                {
                    var result = new UnitTestResult();
                    // DataRow drTestResult = this.dtTestCaseCollection.NewRow();


                    if (selectedNode.Attributes != null && selectedNode.Attributes["dataRowInfo"] != null)
                        result.TestRun = selectedNode.Attributes["dataRowInfo"].Value;

                    if (selectedNode.Attributes != null)
                    {
                        result.TestName = selectedNode.Attributes["testName"].Value;
                        //drTestResult["TestClass"] = node.Attributes[""].Value;
                        result.TestCaseDescription = _testIds[selectedNode.Attributes["testName"].Value];
                        result.TestStatus = selectedNode.Attributes["outcome"].Value;
                    }
                    _unitTestResultCollection.Add(result);
                }
        }

        private void ReadUnitTestResultNodes(XmlNode node, XmlNamespaceManager nsMgr)
        {
            //Trying to find if innerresults exists
            var xDoc = new XmlDocument();
            xDoc.LoadXml(node.OuterXml);

            XmlNodeList innerResultNodeList = xDoc.SelectNodes(InnerResultNodeListXPath, nsMgr);
            if (innerResultNodeList != null && innerResultNodeList.Count > 0)
            {
                foreach (XmlNode innerResultNode in innerResultNodeList)
                {
                    var unitTestNodeList = innerResultNode.SelectNodes(InnerUnitTestResultXPath, nsMgr);
                    if (unitTestNodeList != null)
                        foreach (XmlNode utResultNode in unitTestNodeList)
                        {
                            ReadUnitTestResultNodes(utResultNode, nsMgr);
                        }
                }
            }
            else
            {
                //build information about test case here
                var result = new UnitTestResult();

                if (node.Attributes != null && node.Attributes["dataRowInfo"] != null)
                    result.TestRun = node.Attributes["dataRowInfo"].Value;

                if (node.Attributes != null)
                {
                    result.TestName = node.Attributes["testName"].Value;
                    //drTestResult["TestClass"] = node.Attributes[""].Value;
                    result.TestCaseDescription = _testIds[node.Attributes["testName"].Value].Replace(",", ", ");
                    result.TestStatus = node.Attributes["outcome"] != null ? node.Attributes["outcome"].Value : "";
                }

                //set Outputnode
                XmlNode outputNode = xDoc.SelectSingleNode(OutputXPath, nsMgr);

                var xmlNodeList = xDoc.SelectNodes(StandardOutputXPath, nsMgr);
                if (xmlNodeList != null && xmlNodeList.Count > 0)
                    result.StandardOutput = (xmlNodeList)[0].InnerText;
                var selectNodes = xDoc.SelectNodes(ErrorInfoXPath, nsMgr);
                if (selectNodes != null && selectNodes.Count > 0)
                    result.ErrorInformation = (selectNodes)[0].InnerText;
                //drTestResult["ErrorInformation"] = node.Attributes[""].Value;

                _unitTestResultCollection.Add(result);
            }
        }

        public UnitTestResultCollection UnitTestCollection
        {
            get { return _unitTestResultCollection; }
        }

        public TestSummary TestSummary
        {
            get { return new TestSummary(_unitTestResultCollection);}
        }


        
    }
}
