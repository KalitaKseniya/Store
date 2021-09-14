using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey(nameof(Provider))]
        public int ProviderId { get; set; }
        public List<User> Users { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
