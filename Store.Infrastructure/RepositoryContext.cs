using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Infrastructure.Configuration;

namespace Store.Infrastructure
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ShoppingCartItem> Items { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> opt) : base(opt)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(obj => obj.Price).HasPrecision(15, 2);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder
                .Entity<Product>()
                .HasMany(p => p.Users)
                .WithMany(u => u.Products)
                .UsingEntity<ShoppingCartItem>(
                    j => j
                         .HasOne(sci => sci.User)
                         .WithMany(u => u.ShoppingCartItems)
                         .HasForeignKey(sci => sci.UserId),
                      j => j
                         .HasOne(sci => sci.Product)
                         .WithMany(p => p.ShoppingCartItems)
                         .HasForeignKey(sci => sci.ProductId),
                    j => {
                        j.HasKey(k => k.Id );
                        j.ToTable("ShoppingCartItems");
                        j.Property(sci => sci.Quantity).HasDefaultValue(1);
                     }
                );
        }
    }
}
