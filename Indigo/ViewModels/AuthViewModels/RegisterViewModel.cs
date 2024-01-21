using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels.AuthViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Fullname is required"),
            MaxLength(64, ErrorMessage = "Limit with 64 symbols")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required"),
            MaxLength(48, ErrorMessage = "Limit with 48 symbols")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required"),
            MaxLength(32, ErrorMessage = "Limit with 32 symbols")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required"),
            MinLength(6, ErrorMessage = "Minimum length must be 6"),
            RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should be: a-z, A-Z, 0-9, and minimum length of 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required"),
            MinLength(6, ErrorMessage = "Minimum length must be 6"),
            RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should be: a-z, A-Z, 0-9, and minimum length of 6"),
            Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
