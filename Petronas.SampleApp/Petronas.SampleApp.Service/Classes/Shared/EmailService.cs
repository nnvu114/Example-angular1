using Microsoft.AspNet.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Service.Classes.Shared
{
    public class EmailService : IIdentityMessageService
    {
        readonly ConcurrentQueue<SmtpClient> _clients = new ConcurrentQueue<SmtpClient>();

        public async Task SendAsync(IdentityMessage message)
        {
            var client = GetOrCreateSmtpClient();

            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPUser]);
                mailMessage.To.Add(new MailAddress(message.Destination));
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Body;

                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                // there can only ever be one-1 concurrent call to SendMailAsync
                await client.SendMailAsync(mailMessage);
            }
            finally
            {
                _clients.Enqueue(client);
            }
        }


        private SmtpClient GetOrCreateSmtpClient()
        {
            SmtpClient mailer = null;

            if (_clients.TryDequeue(out mailer))
            {
                return mailer;
            }

            int port = 80;
            int.TryParse(ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPPort], out port);

            mailer = new SmtpClient(ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPServer], port);
            mailer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPUser], ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPPassword]);
            mailer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings[Common.Constants.Configuration.SMTPEnableSSL]);

            return mailer;
        }
    }
}
