
namespace Store.Core.DTO
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
