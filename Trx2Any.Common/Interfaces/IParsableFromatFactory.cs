using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.Interfaces
{
    public interface IParsableFromatFactory
    {
        ITrxParsable GetProvider(string factoryFormat, string trxPath);
    }
}
