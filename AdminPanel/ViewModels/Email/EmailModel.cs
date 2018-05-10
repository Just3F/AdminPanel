using System.Collections.Generic;

namespace AdminPanel.ViewModels.Email
{
    public class EmailModel
    {
        public EmailModel()
        {
            Attachments = new List<AttachmentItem>();
        }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public List<AttachmentItem> Attachments { get; set; }
    }
}
