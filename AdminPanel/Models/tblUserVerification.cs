using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Models
{
    public class tblUserVerification : DatabaseObject
    {
        public DateTime? SentEmailCodeTime { get; set; }
        public DateTime? SentPhoneCodeTime { get; set; }
        public string EmailCode { get; set; }
        public string PhoneCode { get; set; }
        public bool EmailActivated { get; set; }
        public bool PhoneActivated { get; set; }
    }
}
