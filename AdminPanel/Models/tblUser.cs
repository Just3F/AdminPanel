using AdminPanel.Const;

namespace AdminPanel.Models
{
    public class tblUser : DatabaseObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }
}
