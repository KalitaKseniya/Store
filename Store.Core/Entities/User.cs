using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey(nameof(ShoppingCart))]
        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
