using System;
using System.ComponentModel.Composition;
using Trx2Any.Common.Interfaces;
using log4net;
using log4net.Config;

namespace Trx2Any.Common
{
    [Export(typeof(IExceptionManager))]
    public class ExceptionManager : IExceptionManager
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(ExceptionManager));

        public ExceptionManager()
        {
            XmlConfigurator.Configure();
        }

        public void HandleException(Exception exception)
        {

            try
            {
                if (exception.GetType() == typeof(FormatException))
                {
                    Logger.Error("Format Exception.");
                }
                else if (exception.GetType() == typeof(InvalidOperationException))
                {
                    Logger.Error("Possible MEF Exception.");
                }
                else if (exception.GetType() == typeof(ImportCardinalityMismatchException))
                {
                    Logger.Error("MEF Exception.");
                }
                //TODO: Add more exception types.

                Logger.Error(exception.Message);
                Logger.Error(exception.StackTrace);
            }
            catch
            {}
        }
    }
}
