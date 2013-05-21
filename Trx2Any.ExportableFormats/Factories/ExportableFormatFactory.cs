using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Trx2Any.Common.Interfaces;

namespace Trx2Any.ExportableFormats.Factories
{
    [Export(typeof(IExportableFormatFactory))]
    public class ExportableFormatFactory : IExportableFormatFactory
    {
        [ImportMany]
        private IEnumerable<Lazy<IExportableFormat, IExportableFormatMetadata>> _exportableFormats;
        
        public bool IsBootStrapped { get; set; }

        public ExportableFormatFactory()
        {
            IsBootStrapped = false;
        }

        bool IExportableFormatFactory.IsBootStrapped { get; set; }

        public IExportableFormat GetProvider(string factoryFormat)
        {
            IExportableFormat identifiedFormat = null;
            try
            {
                foreach (Lazy<IExportableFormat, IExportableFormatMetadata> format in _exportableFormats)
                {
                    if (!format.Metadata.FormatName.Equals(factoryFormat, StringComparison.OrdinalIgnoreCase)) continue;
                    identifiedFormat = format.Value;
                    break;
                }
                if (identifiedFormat == null)
                {
                    throw new Exception("Format not available");
                }
            }
            catch (Exception)
            {
                throw new Exception("Exportable Formats cannot be found/identified");
            }
            return identifiedFormat;
        }
    }
}