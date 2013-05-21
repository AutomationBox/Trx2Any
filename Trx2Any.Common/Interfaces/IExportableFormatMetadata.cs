using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.Interfaces
{
    //Used to add metadata in a stronlgy typed fashion
    public interface IExportableFormatMetadata 
    {
        string FormatName{get;}
    }
}
