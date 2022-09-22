using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class ProfileUpdate
    {
        public string ImgUrl { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Patner name")]
        [Required(ErrorMessage = "Patner name required")]
        public string PatnerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number required")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}