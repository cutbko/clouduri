using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace CloudUri.Web.ViewModels
{
    public class SingUpViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, enter your username")]
        [MinLength(4, ErrorMessage = "Your user name must be at least 4 symbols")]
        [MaxLength(12, ErrorMessage = "Your user name can be up to 12 symbols")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, enter your Email")]
        [Display(Name = "User name")]
        [Email(ErrorMessage = "Email is in incorrect format")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, enter your password")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 symbols")]
        [MaxLength(30, ErrorMessage = "30 symbols is not enough to you?")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, confirm your password")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 symbols")]
        [MaxLength(30, ErrorMessage = "30 symbols is not enough to you?")]
        [Display(Name = "Password confirm")]
        [EqualTo("Password")]
        public string PasswordConfirm { get; set; }
    }
}