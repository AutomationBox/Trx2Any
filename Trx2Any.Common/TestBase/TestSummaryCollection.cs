using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.TestBase
{
    public class TestSummaryCollection : List<TestSummary>
    {
        public void Add(TestSummary summary)
        {
            this.Add(summary);
        }

        public void Remove(int index)
        {
            if (index > this.Count - 1 || index < 0)
            {
                Console.WriteLine("index is out of range");
            }
            else
            {
                this.RemoveAt(index);
            }
        }
        public TestSummary Item(int index)
        {
            return (TestSummary)this[index];
        }

    }
}
