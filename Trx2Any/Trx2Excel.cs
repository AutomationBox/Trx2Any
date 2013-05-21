using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Trx2Any.Common;
using Trx2Any.Common.Interfaces;

namespace Trx2Any.Presentation.ConsoleMode
{
    [Export(typeof (IEntryPoint))]
    public class Trx2Excel : Trx2Any
    {
        private readonly IExceptionManager _exceptionManager;

        [ImportingConstructor]
        public Trx2Excel([Import("ExceptionManager")] IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        public override void BeginExecution()
        {
            try
            {
                base.BeginExecution();
                var parsableFactory = MEFServiceLocator.Instance.GetInstance<IParsableFromatFactory>();
                var trx = parsableFactory.GetProvider("MSTest2010Trx", _trxPath);

                var exportableFormatFactory = MEFServiceLocator.Instance.GetInstance<IExportableFormatFactory>();
                var export = exportableFormatFactory.GetProvider("excel2007");

                var isExported = export.ExportData(trx.TestSummary, trx.UnitTestCollection, _outputFile);
                if (!isExported)
                    throw new Exception("Above trx could not be exported to excel");
            }
            catch (Exception ex)
            {
                _exceptionManager.HandleException(ex);
            }
        }

        public override void SendMail(List<string> trxFiles, string outputFile,
                                      string customBody)
        {
            try
            {
                base.SendMail(trxFiles, outputFile, customBody);
            }
            catch (Exception exception)
            {
                _exceptionManager.HandleException(exception);
            }
        }
    }
}