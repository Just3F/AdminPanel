using System;
using System.ComponentModel.DataAnnotations.Schema;
using AdminPanel.Const;

namespace AdminPanel.Models
{
    public class tblUser : DatabaseObject
    {
        public tblUser()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
        public DateTime DateCreated { get; set; }

        public long UserVerificationId { get; set; }
        [ForeignKey("UserVerificationId")]
        public tblUserVerification UserVerification { get; set; }
    }
}
