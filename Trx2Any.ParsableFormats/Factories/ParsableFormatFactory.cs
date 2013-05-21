using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Trx2Any.Common.Interfaces;

namespace Trx2Any.ParsableFormats.Factories
{
    [Export(typeof (IParsableFromatFactory))]
    public class ParsableFormatFactory : IParsableFromatFactory
    {
        [ImportMany] private IEnumerable<Lazy<ITrxParsable>> _trxParsable;

        public ITrxParsable GetProvider(string factoryFormat, string trxPath)
        {
            ITrxParsable trxType = null;
            try
            {
                foreach (Lazy<ITrxParsable> trx in _trxParsable)
                {
                    var val = trx.Value;
                    if (val.GetType().Name.Equals(factoryFormat, StringComparison.OrdinalIgnoreCase))
                        trxType = val;
                    //switch (factoryFormat.ToLower())
                    //{
                    //    case "mstest2010trx":
                    //        if (val.GetType().Name.Equals(factoryFormat, StringComparison.OrdinalIgnoreCase))
                    //            trxType = val;
                    //        break;
                    //}
                }
                if (trxType == null)
                    throw new FormatException("This format is not known");
            }
            catch (Exception)
            {
                throw new Exception("Parsable type cannot be found/identified");
            }
            return trxType;
        }
    }
}