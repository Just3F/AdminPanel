using System;

namespace AdminPanel.ViewModels.Users
{
    public class UserVerificationViewModel
    {
        public long PKID { get; set; }
        public DateTime? SentEmailCodeTime { get; set; }
        public DateTime? SentPhoneCodeTime { get; set; }
        public string EmailCode { get; set; }
        public string PhoneCode { get; set; }
        public bool EmailActivated { get; set; }
        public bool PhoneActivated { get; set; }
    }
}
