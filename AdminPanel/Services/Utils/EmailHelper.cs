using System;
using System.IO;
using System.Threading.Tasks;
using AdminPanel.ViewModels.Email;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace AdminPanel.Services.Utils
{

    public class EmailHelper
    {
        public static void Init()
        {
            _SmtpServer = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:SmtpServer").Get<string>();
            _SmtpPort = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:SmtpPort").Get<int>();
            _LoginEmail = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginEmail").Get<string>();
            _AdminEmail = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:AdminEmail").Get<string>();
            _LoginPassword = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginPassword").Get<string>();
            _LoginDomain = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginPassword").Get<string>();
            _EnableSSL = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:EnableSSL").Get<bool>();
            _HtmlFormat = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:HtmlFormat").Get<bool>();
            _TestEmailFrom = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:TestEmailFrom").Get<string>();
            _TestEmailTo = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:TestEmailTo").Get<string>();
            _TestEmailTo = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:TestEmailTo").Get<string>();
            _BccAll = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:BccAll").Get<string>();
            _SiteURL = ConfigurationAccessor.Congiguration.GetSection("DefaultValues:SiteURL").Get<string>();
        }

        public static string WebRootPath = "";
        private const string DefaultNoReplyAddress = "noreply@test.com.au";
        private static string _SmtpServer;
        private static int _SmtpPort;
        public static string _AdminEmail;
        public static string _SiteURL;
        private static string _LoginEmail;
        private static string _LoginPassword;
        private static string _LoginDomain;
        private static bool _EnableSSL;
        private static bool _HtmlFormat;

        private static string _BccAll;
        private static string _TestEmailFrom;
        private static string _TestEmailTo;
        public static void SendEmail(SentEmailItem mail)
        {
            var emailMessage = new MimeMessage();
            mail.EmailFrom = _TestEmailFrom;

            emailMessage.From.Add(new MailboxAddress(mail.EmailFrom));

            if (string.IsNullOrEmpty(_BccAll))
            {
                foreach (var bcc in mail.BccList)
                {
                    emailMessage.Bcc.Add(new MailboxAddress(bcc));
                }
            }
            else
            {
                emailMessage.Bcc.Add(new MailboxAddress(_BccAll));
            }

            foreach (var cc in mail.CcList)
            {
                emailMessage.Cc.Add(new MailboxAddress(cc));
            }

            if (string.IsNullOrEmpty(_TestEmailTo))
            {
                foreach (var to in mail.EmailToList)
                {
                    emailMessage.To.Add(new MailboxAddress(to, to));
                }
            }
            else
            {
                emailMessage.To.Add(new MailboxAddress(_TestEmailTo));
            }


            var multipart = new Multipart("mixed");

            emailMessage.Subject = mail.Subject;
            var body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = mail.Body
            };
            multipart.Add(body);

            foreach (var attachment in mail.Attachments)
            {
                var att = new MimePart()
                {
                    ContentObject = new ContentObject(File.OpenRead(WebRootPath + attachment.Path)),
                    FileName = attachment.Name
                };
                multipart.Add(att);
            }

            emailMessage.Body = multipart;

            using (var client = new SmtpClient())
            {
                client.Connect(_SmtpServer, _SmtpPort, _EnableSSL);
                client.Authenticate(_LoginEmail, _LoginPassword);
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
        public static async Task SendEmailAsync(SentEmailItem mail)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(mail.EmailFrom));

            if (string.IsNullOrEmpty(_BccAll))
            {
                foreach (var bcc in mail.BccList)
                {
                    emailMessage.Bcc.Add(new MailboxAddress(bcc));
                }
            }
            else
            {
                emailMessage.Bcc.Add(new MailboxAddress(_BccAll));
            }

            foreach (var cc in mail.CcList)
            {
                emailMessage.Cc.Add(new MailboxAddress(cc));
            }

            if (string.IsNullOrEmpty(_TestEmailTo))
            {
                foreach (var to in mail.EmailToList)
                {
                    emailMessage.To.Add(new MailboxAddress(to, to));
                }
            }
            else
            {
                emailMessage.To.Add(new MailboxAddress(_TestEmailTo));
            }


            var multipart = new Multipart("mixed");

            emailMessage.Subject = mail.Subject;
            var body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = mail.Body
            };
            multipart.Add(body);

            foreach (var attachment in mail.Attachments)
            {
                var att = new MimePart()
                {
                    ContentObject = new ContentObject(File.OpenRead(WebRootPath + attachment.Path)),
                    FileName = attachment.Name
                };
                multipart.Add(att);
            }

            emailMessage.Body = multipart;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_SmtpServer, _SmtpPort, _EnableSSL);
                await client.AuthenticateAsync(_LoginEmail, _LoginPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }

    public static class Context
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        private static Uri GetAbsoluteUri()
        {
            var request = HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

    }

    public class ConfigurationAccessor
    {
        public static IConfiguration Congiguration;

    }
}
