namespace Trx2Any.Common.Interfaces.ViewModels
{
    public interface ITrx2AnyViewModel
    {
        string ParsedFilePath { get; set; }
        bool IsSendMail { get; set; }
        string OutputFilePath { get; set; }
        string ParsableFormat { get; set; }
        string ExportableFormat { get; set; }
    }
}
