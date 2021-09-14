using System;
using System.Collections.Generic;

namespace Store.Core.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Product> Products { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
