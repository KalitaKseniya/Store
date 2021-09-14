
namespace Store.Core.Entities
{
    public class ShoppingCartItem
    {
        public User User { get; set; } 
        public string UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
