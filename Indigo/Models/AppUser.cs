using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Indigo.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Fullname is required"), MaxLength(64)]
        public string Fullname { get; set; }
    }
}
