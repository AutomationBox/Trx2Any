using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trx2Any.Common.TestBase
{
    public class UnitTestResult
    {
        private String _TestName;
        private String _TestCategory;
        private string _TestStatus;

        public string TestStatus
        {
            get { return _TestStatus; }
            set { _TestStatus = value; }
        }
        private string _TestRun;

        public string TestRun
        {
            get { return _TestRun; }
            set { _TestRun = value; }
        }
        private string _ErrorInformation;
        private string _StandardOutput;
        private string _TestCaseDescription;

        public string TestCaseDescription
        {
            get { return _TestCaseDescription; }
            set { _TestCaseDescription = value; }
        }

        public UnitTestResult()
        {
        }
        public UnitTestResult(String TestName, String TestCategory, string TestStatus,string TestRun)
        {
            this._TestName = TestName;
            this._TestCategory = TestCategory;
            this._TestStatus = TestStatus;
            this._TestRun = TestRun;
        }



        public String TestName
        {
            get
            {
                return _TestName;
            }
            set { this._TestName = value; }
        }


        public string ErrorInformation
        {
            get { return _ErrorInformation; }
            set { _ErrorInformation = value; }
        }


        public string StandardOutput
        {
            get { return _StandardOutput; }
            set { _StandardOutput = value; }
        }

    }
    
}
