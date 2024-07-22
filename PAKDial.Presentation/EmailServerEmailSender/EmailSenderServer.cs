using PAKDial.Domains.SecrateKeys;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PAKDial.Presentation.EmailServerEmailSender
{
    public static class EmailSenderServer
    {

        public static Task Execute(string subject, string message, string email)
        {
            var client = new SendGridClient(SecretKeys.EmailApiKey);
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
