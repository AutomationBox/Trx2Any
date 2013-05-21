using Trx2Any.Common.TestBase;

namespace Trx2Any.Common.Interfaces
{
    public interface ITrxParsable
    {
       //void ReturnUnitTestCollection(UnitTestResultCollection UnitTestResultCollection);
       //void ReturnTestSummaryCollection(TestSummaryCollection TestSummaryCollection);

       UnitTestResultCollection UnitTestCollection { get; }
       TestSummary TestSummary { get; }
    }
}
