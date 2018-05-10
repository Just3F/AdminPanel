using System;
using AdminPanel.Const;

namespace AdminPanel.ViewModels.Users
{
    public class UserViewModel
    {
        public long PKID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
        public DateTime DateCreated { get; set; }
        public long UserVerificationId { get; set; }
        public UserVerificationViewModel UserVerification { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}
