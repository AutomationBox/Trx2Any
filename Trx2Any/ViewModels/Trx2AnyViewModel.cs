using Trx2Any.Common.Interfaces.ViewModels;

namespace Trx2Any.Presentation.ConsoleMode.ViewModels
{
    class Trx2AnyViewModel : ITrx2AnyViewModel
    {
        public string ParsedFilePath { get; set; }
        public bool IsSendMail { get; set; }
        public string OutputFilePath { get; set; }
        public string ParsableFormat { get; set; }
        public string ExportableFormat { get; set; }
    }
}
