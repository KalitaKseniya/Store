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
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
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
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(sc => sc.Products)
                .WithMany(p => p.ShoppingCarts)
                .UsingEntity<ShoppingCartItem>(
                    j => j.HasOne(sci => sci.Product)
                           .WithMany(p => p.Items)
                           .HasForeignKey(sci => sci.ProductId),
                    j =>
                    {
                        return j.HasOne(sci => sci.ShoppingCart)
                         .WithMany(p => p.ShoppingCartItems)
                         .HasForeignKey(sci => sci.ShoppingCartId);
                    },
                    j => {
                        j.HasKey(k => new { k.ShoppingCartId, k.ProductId });
                        j.ToTable("ShoppingCartItems");
                     }
                );
            modelBuilder.Entity<ShoppingCart>()
                        .Property(sc => sc.DateCreated)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
