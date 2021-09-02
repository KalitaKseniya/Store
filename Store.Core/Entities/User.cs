using Microsoft.AspNetCore.Identity;

namespace Store.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
