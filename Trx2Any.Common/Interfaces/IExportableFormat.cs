using System.ComponentModel.Composition;
using Trx2Any.Common.TestBase;

namespace Trx2Any.Common.Interfaces
{
    
    public interface IExportableFormat
    {
        bool ExportData(TestSummary testSummaryCollection,
                        UnitTestResultCollection UnitTestResultCollection,
                        string fileName);
     }
}
