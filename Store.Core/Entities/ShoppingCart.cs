using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Core.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Product> Products { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
