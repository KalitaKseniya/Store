using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;

namespace Store.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Little black dress",
                Description = "A beautiful black dress.",
                Price = 100,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Green dress",
                Description = "A long green dress for little girls. 100% cotton.",
                Price = 50,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Skinny jeans",
                Description = "Skinny blue jeans.",
                Price = 40,
                CategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Leggins",
                Description = "Leggins for sport.",
                Price = 15,
                CategoryId = 3
            },
            new Product
            {
                Id = 5,
                Name = "T-shirt Black",
                Description = "A black T-shirt for sport.",
                Price = 25,
                CategoryId = 3
            },
            new Product
            {
                Id = 6,
                Name = "T-shirt White",
                Description = "A white T-shirt for sport.",
                Price = 22,
                CategoryId = 3
            }
        );
        }

    }
}
