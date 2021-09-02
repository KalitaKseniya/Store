using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;

namespace Store.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Dresses"
            },
            new Category
            {
                Id = 2,
                Name = "Jeans"
            },
            new Category
            {
                Id = 3,
                Name = "Sport clothes"
            }
        );
        }

    }
}
