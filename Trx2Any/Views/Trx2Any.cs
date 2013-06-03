using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Trx2Any.Common;
using Trx2Any.Common.Interfaces;
using Trx2Any.Common.Interfaces.ViewModels;

namespace Trx2Any.Presentation.ConsoleMode.Views
{
    [Export(typeof(IEntryPoint))]
    public class Trx2Any : IEntryPoint
    {
        private readonly IExceptionManager _exceptionManager;
        private readonly ITrx2AnyViewModel _viewModel;

        [ImportingConstructor]
        public Trx2Any([Import("ExceptionManager")]IExceptionManager exceptionManager, [Import("Trx2AnyViewModel")]ITrx2AnyViewModel viewModel)
        {
            _exceptionManager = exceptionManager;
            _viewModel = viewModel;
        }

        public virtual ExitCode BeginExecution()
        {
            try
            {
                if (String.IsNullOrEmpty(_viewModel.ParsableFormat))
                    _viewModel.ParsableFormat = ConfigurationManager.AppSettings.Get("ParsableFormat");
                if (String.IsNullOrEmpty(_viewModel.ExportableFormat))
                    _viewModel.ExportableFormat = ConfigurationManager.AppSettings.Get("ExportableFormat");

                bool isIncludeOuput = Convert.ToBoolean(
                    ConfigurationManager.AppSettings.Get("IncludeOutputInReport").ToString());

                var parsableFactory = MEFServiceLocator.Instance.GetInstance<IParsableFromatFactory>();
                var trx = parsableFactory.GetProvider(_viewModel.ParsableFormat, _viewModel.ParsedFilePath);

                var exportableFormatFactory = MEFServiceLocator.Instance.GetInstance<IExportableFormatFactory>();
                var export = exportableFormatFactory.GetProvider(_viewModel.ExportableFormat);

                var isExported = export.ExportData(trx.TestSummary, trx.UnitTestCollection, _viewModel.OutputFilePath, isIncludeOuput);
                if (!isExported)
                {
                    throw new Exception("Above trx could not be exported to excel");
                }
                return ExitCode.Success;
            }
            catch (Exception ex)
            {
                _exceptionManager.HandleException(ex);
                return ExitCode.Failure;
            }
        }

        public virtual void SendMail(List<string> trxFiles, string outputFile, string customBody)
        {
            string to = ConfigurationManager.AppSettings["ResultRecipientsTo"].ToString(CultureInfo.InvariantCulture);
            string cc = ConfigurationManager.AppSettings["ResultRecipientsCC"].ToString(CultureInfo.InvariantCulture);
            string bcc = ConfigurationManager.AppSettings["ResultRecipientsBCC"].ToString(CultureInfo.InvariantCulture);
            string subject = String.Format(ConfigurationManager.AppSettings["Subject"].ToString(CultureInfo.InvariantCulture),
                                           DateTime.Now.ToString("dddd, dd MMMM yyyy, HH:mm tt"));

            string body = customBody;
            //Changed to parsed body
            string from = ConfigurationManager.AppSettings["From"].ToString();
            trxFiles.Add(outputFile);
            List<Attachment> atchList = this.GetAttachment(trxFiles);

            this.SendMailMessage(from, to, bcc, cc, subject, body, atchList);
            Console.WriteLine("Mail sent successfully to : " + to);
        }

        private void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body,
                                     ICollection<Attachment> atchList)
        {
            var mailObject = new MailMessage();
            var smtpClient = new SmtpClient();
            //MailMessage mailObject = new MailMessage();
            mailObject = PrepareMailMessage(from, to, bcc, cc, subject, body, atchList);

            var smtp =
                new SmtpClient(ConfigurationManager.AppSettings["Host"],
                                               int.Parse(ConfigurationManager.AppSettings["Port"]))
                    {
                        EnableSsl = true,
                        Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"],
                                                            ConfigurationManager.AppSettings["Password"])
                    };
            smtp.Send(mailObject);
            mailObject.Dispose();
            GC.Collect();
        }

        private MailMessage PrepareMailMessage(string from, string to, string bcc, string cc, string subject,
                                               string body, ICollection<Attachment> atchList)
        {
            var mailBody = new MailMessage {Body = body, Subject = subject, From = new MailAddress(@from)};

            if (!String.IsNullOrEmpty(to))
                mailBody.To.Add(to);

            if (!String.IsNullOrEmpty(cc))
                mailBody.CC.Add(cc);

            if (!String.IsNullOrEmpty(bcc))
                mailBody.Bcc.Add(bcc);

            mailBody.Priority = MailPriority.Normal;
            //set mails body format
            mailBody.IsBodyHtml = true;
            if (atchList != null && atchList.Count > 0)
            {
                foreach (var item in atchList)
                {
                    mailBody.Attachments.Add(item);
                }
            }
            mailBody.BodyEncoding = Encoding.UTF8;
            return mailBody;
        }

        private List<Attachment> GetAttachment(IEnumerable<string> fileName)
        {
            var atchList = new List<Attachment>();

            var ct = new ContentType("application/vnd.xls");

            try
            {
                foreach (string file in fileName)
                {
                    if (File.Exists(file) & ConfigurationManager.AppSettings["sendtrx"] == "true")
                        atchList.Add(new Attachment(file, ct));
                }
            }
            catch (Exception ex)
            {
                //don't throw an error here
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return atchList;
        }
    }
}