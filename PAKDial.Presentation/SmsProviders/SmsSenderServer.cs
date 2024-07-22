using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.SmsProviders
{
    public static class SmsSenderServer
    {
        public static Task Execute(string subject, string message, string email)
        {
            var client = new SendGridClient("9979868767698678987");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("PakDialDevelopment@gmail.com", "Pak Dial"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(msg);
        }
    }
}
