using System;
using Trx2Any.Common;
using Trx2Any.Common.Interfaces.ViewModels;
using Trx2Any.Presentation.ConsoleMode.ViewModels;

namespace Trx2Any.Presentation.ConsoleMode
{
    public class AppStart
    {
        #region Private Fields
        private static ITrx2AnyViewModel _shellViewModel;
        #endregion

        private static int Main(string[] args)
        {
            var body = string.Empty;
            body = "This is an auto generated mail \r\n\r\n";
            body = ("Test case summary : \r\n");
            try
            {
                _shellViewModel = new Trx2AnyViewModel();
                // For each .trx file in the given folder process it
                foreach (string s in args)
                {
                    if (s.StartsWith("/i:"))
                    {
                        _shellViewModel.ParsedFilePath = s.Remove(0, 3);
                    }
                    else if (s.StartsWith("/o:"))
                    {
                        _shellViewModel.OutputFilePath = s.Remove(0, 3);
                    }
                    else if (s.StartsWith("/mail"))
                    {
                        _shellViewModel.IsSendMail = true;
                    }
                    else if (s.StartsWith("/p:"))
                    {
                        _shellViewModel.ParsableFormat = s.Remove(0, 3);
                    }
                    else if (s.StartsWith("/e:"))
                    {
                        _shellViewModel.ExportableFormat = s.Remove(0, 3);
                    }
                    else
                    {
                        throw new Exception("invalid switch " + s);
                    }
                }

                var bootStrapper = new MEFBootstrapper(_shellViewModel);
                var exitCode = bootStrapper.Run();
                System.Diagnostics.Debug.WriteLine(exitCode);
                return (int) exitCode;
            }
            catch (Exception ex)
            {
                var exceptionManager = new ExceptionManager();
                exceptionManager.HandleException(ex);
                return (int) ExitCode.Failure;
            }
        }
    }
}