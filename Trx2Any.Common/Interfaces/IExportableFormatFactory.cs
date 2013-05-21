namespace Trx2Any.Common.Interfaces
{
    public interface IExportableFormatFactory
    {
        bool IsBootStrapped { get; set; }
        IExportableFormat GetProvider(string factoryFormat);
    }
}
