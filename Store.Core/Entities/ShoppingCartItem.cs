
namespace Store.Core.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCart ShoppingCart { get; set; } 
        public int ShoppingCartId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
