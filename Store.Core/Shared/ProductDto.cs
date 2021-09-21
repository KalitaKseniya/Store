
using Store.Core.Entities;

namespace Store.Core.Shared
{
    public class ProductDto : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public int ProviderId { get; set; }

        public ProductDto()
        {

        }

        public ProductDto(Product product, string operation)
        {
            EntityId = product.Id;
            Operation = operation;
            Type = typeof(Product).Name;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            CategoryId = product.CategoryId;
            ProviderId = product.ProviderId;
            ImagePath = product.ImagePath;
        }
    }
}
