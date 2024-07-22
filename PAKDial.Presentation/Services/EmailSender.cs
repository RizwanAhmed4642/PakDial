using PAKDial.Domains.SecrateKeys;
using PAKDial.Presentation.EmailServerEmailSender;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email); ;
        }

        public Task Execute(string subject, string message, string email)
        {
            return EmailSenderServer.Execute(subject,message,email);
        }
    }
}
