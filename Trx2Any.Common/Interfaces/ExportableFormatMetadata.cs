using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.Interfaces
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ExportableFormatMetadata : ExportAttribute
    {
        public ExportableFormatMetadata()
            : base(typeof(IExportableFormat))
        {
        }

        public string FormatName
        {
            get;
            set;
        }
    }
}
