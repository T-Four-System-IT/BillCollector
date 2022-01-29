using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MailServices
{
    public class EmailService
    {
        #region -> Definicão de Campos
        private SmtpClient smtpClient;

        private readonly string Address = "VitalClinicSystem@gmail.com";
        private readonly string Password = "@admin123";
        private readonly string DisplayName = "Vital Clinic";
        private readonly string Host = "smtp.gmail.com";
        private readonly int Port = 587;
        private readonly bool SSL = true;
        #endregion

        #region -> Constructor
        public EmailService()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(Address, Password);
            smtpClient.Host = Host;
            smtpClient.Port = Port;
            smtpClient.EnableSsl = SSL;
        }
        #endregion

        #region -> Métodos
        public void Send(string recipient, string subject, string body)
        {
            var mailMessage = new MailMessage();
            var mailSender = new MailAddress(Address, DisplayName);

            try
            {
                mailMessage.From = mailSender;
                mailMessage.To.Add(recipient);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;

                smtpClient.Send(mailMessage);
            }
            catch (SmtpException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
        public void Send(List<string> recipients, string subject, string body)
        {

            var mailMessage = new MailMessage();
            var mailSender = new MailAddress(Address, DisplayName);

            try
            {
                mailMessage.From = mailSender;
                foreach (var recipientMail in recipients)
                {
                    mailMessage.To.Add(recipientMail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;

                smtpClient.Send(mailMessage);
            }
            catch (SmtpException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
        #endregion
    }
}
