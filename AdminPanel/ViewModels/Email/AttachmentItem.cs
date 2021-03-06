﻿namespace AdminPanel.ViewModels.Email
{
    public class AttachmentItem
    {
        public long PKID { get; set; }
        public long? EmailId { get; set; }
        public string Path { get; set; }

        public string Name => System.IO.Path.GetFileName(Path);
    }
}
