using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.TestBase
{
    
    public class TestSummary
    {
        private double passPercentage;
        
        private int totalTestCaseRun;
        public int TotalTestCaseRun
        {
            get { return totalTestCaseRun; }
            set { totalTestCaseRun = value; }
        }

        private int passed;
        public int Passed
        {
            get { return passed; }
            set { passed = value; }
        }

        private int failed;
        public int Failed
        {
            get { return failed; }
            set { failed = value; }
        }

        private int inconclusive;
        public int Inconclusive
        {
            get { return inconclusive; }
            set { inconclusive = value; }
        }

        public TestSummary()
        {
            passPercentage = 0.0d;
            totalTestCaseRun = 0;
            passed = 0;
            failed = 0;
            inconclusive = 0;
        }

        public TestSummary(UnitTestResultCollection results)
        {
            totalTestCaseRun = results.Count;
            passed = results.Where(x => x.TestStatus.Equals("passed", StringComparison.OrdinalIgnoreCase)).Count();
            failed = results.Where(x => x.TestStatus.Equals("failed", StringComparison.OrdinalIgnoreCase)).Count();
            inconclusive = totalTestCaseRun - passed - failed;
            passPercentage = passed / totalTestCaseRun * 100;
        }
   
    }

    
}
