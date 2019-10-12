using AdwardSoft.Provider.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace AdwardSoft.Provider.Helper
{
    public class MailHelper
    {
        private IOptions<MailOpt> _mailHelper;

        public MailHelper(IOptions<MailOpt> mailHelper)
        {
            _mailHelper = mailHelper;
        }

        public bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = _mailHelper.Value.SMTPHost;
                var port = _mailHelper.Value.SMTPPort;
                var fromEmail = _mailHelper.Value.FromEmailAddress;
                var fromName = _mailHelper.Value.FromName;
                var password = _mailHelper.Value.FromEmailPassword;

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
