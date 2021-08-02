using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;

namespace Store.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> opt) : base(opt)
        { }
    }
}
