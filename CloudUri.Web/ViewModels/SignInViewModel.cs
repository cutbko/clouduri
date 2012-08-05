using System.ComponentModel.DataAnnotations;

namespace CloudUri.Web.ViewModels
{
    /// <summary>
    /// Sign in View Model
    /// </summary>
    public class SignInViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, enter your username")]
        [MinLength(4, ErrorMessage = "Your user name must be at least 4 symbols")]
        [MaxLength(12, ErrorMessage = "Your user name can be up to 12 symbols")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, enter your password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool StayLoggedIn { get; set; }
    }
}