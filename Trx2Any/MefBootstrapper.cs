using System;
using System.ComponentModel.Composition;
using System.Configuration;
using Trx2Any.Common;
using Trx2Any.Common.Interfaces;
using Trx2Any.Common.Interfaces.ViewModels;

namespace Trx2Any.Presentation.ConsoleMode
{
    public class MEFBootstrapper
    {
        private readonly ITrx2AnyViewModel _shellViewModel;

        public MEFBootstrapper(ITrx2AnyViewModel shellViewModel)
        {
            _shellViewModel = shellViewModel;
        }

        public ExitCode Run()
        {
            MEFServiceLocator.Instance.Initialize(ConfigurationManager.AppSettings["ExtensionPath"]);
            MEFServiceLocator.Instance.Container.ComposeExportedValue("ParsedFilePath", _shellViewModel.ParsedFilePath);
            MEFServiceLocator.Instance.Container.ComposeExportedValue("Trx2AnyViewModel", _shellViewModel);
            var exceptionManager = MEFServiceLocator.Instance.GetInstance<IExceptionManager>();
            MEFServiceLocator.Instance.Container.ComposeExportedValue("ExceptionManager", exceptionManager);

            var entryPoint = MEFServiceLocator.Instance.GetInstance<IEntryPoint>();
            if (entryPoint == null)
            {
                throw new InvalidOperationException("No entry point found. Add an IEntryPoint to the container.");
            }

            // Invoke the entry point
            return entryPoint.BeginExecution();
        }
    }
}
