using Microsoft.Extensions.Configuration;

namespace AdminPanel.Services.Utils
{
    public class ConfigurationAccessor
    {
        public static IConfiguration Congiguration;

        public static string WebRootPath = "";
        public const string DefaultNoReplyAddress = "noreply@test.com.au";
        public static string SmtpServer;
        public static int SmtpPort;
        public static string AdminEmail;
        public static string SiteURL;
        public static string LoginEmail;
        public static string LoginPassword;
        public static string LoginDomain;
        public static bool EnableSSL, HtmlFormat;

        public static string EmailFrom, TestEmailTo, BccAll;
        public static void Init()
        {
            SmtpServer = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:SmtpServer").Get<string>();
            SmtpPort = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:SmtpPort").Get<int>();
            LoginEmail = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginEmail").Get<string>();
            AdminEmail = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:AdminEmail").Get<string>();
            LoginPassword = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginPassword").Get<string>();
            LoginDomain = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:LoginPassword").Get<string>();
            EnableSSL = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:EnableSSL").Get<bool>();
            HtmlFormat = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:HtmlFormat").Get<bool>();
            EmailFrom = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:EmailFrom").Get<string>();
            TestEmailTo = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:TestEmailTo").Get<string>();
            TestEmailTo = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:TestEmailTo").Get<string>();
            BccAll = ConfigurationAccessor.Congiguration.GetSection("EmailSettings:BccAll").Get<string>();
            SiteURL = ConfigurationAccessor.Congiguration.GetSection("DefaultValues:SiteURL").Get<string>();
        }
    }
}
