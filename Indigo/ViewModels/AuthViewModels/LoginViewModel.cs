using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels.AuthViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required"),
            MaxLength(64, ErrorMessage = "Limit with 64 symbols")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required"),
            MinLength(6, ErrorMessage = "Minimum length must be 6"),
            RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should be: a-z, A-Z, 0-9, and minimum length of 6")]
        public string Password { get; set; }
    }
}
