using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class UserForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        [Required(ErrorMessage = "Username is a required field.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
