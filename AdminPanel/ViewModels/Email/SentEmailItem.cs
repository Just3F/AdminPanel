using System.Collections.Generic;
using System.Linq;

namespace AdminPanel.ViewModels.Email
{
    public class SentEmailItem
    {
        public SentEmailItem()
        {
            Attachments = new List<AttachmentItem>();
        }

        public long PKID { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public List<string> EmailToList => !string.IsNullOrEmpty(EmailTo) ? (EmailTo ?? string.Empty).Split(',').ToList() : new List<string>();

        public List<string> CcList => !string.IsNullOrEmpty(Cc) ? (Cc ?? string.Empty).Split(',').ToList() : new List<string>();

        public List<string> BccList => !string.IsNullOrEmpty(Bcc) ? (Bcc ?? string.Empty).Split(',').ToList() : new List<string>();

        public List<AttachmentItem> Attachments { get; set; }
    }
}
