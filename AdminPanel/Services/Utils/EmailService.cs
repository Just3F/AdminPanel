using System.Threading.Tasks;
using AdminPanel.ViewModels.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace AdminPanel.Services.Utils
{
    public class EmailService
    {
        public async Task SendEmailAsync(EmailModel email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Administration of AdminPanel", ConfigurationAccessor.EmailFrom));
            emailMessage.To.Add(new MailboxAddress("", email.EmailTo));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = email.Body,
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(ConfigurationAccessor.SmtpServer, ConfigurationAccessor.SmtpPort, ConfigurationAccessor.EnableSSL);
                await client.AuthenticateAsync(ConfigurationAccessor.LoginEmail, ConfigurationAccessor.LoginPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
