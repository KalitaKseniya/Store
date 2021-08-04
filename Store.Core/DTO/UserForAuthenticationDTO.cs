using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class UserForAuthenticationDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } 
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
