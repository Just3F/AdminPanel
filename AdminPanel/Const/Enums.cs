using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Const
{
    public enum Role
    {
        [Display(Name = "Administrator")]
        Administrator = 1,
        [Display(Name = "User")]
        User = 2
    }
}
