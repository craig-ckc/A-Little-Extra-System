using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email required")]
        public string Email {get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}