using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Infrastructure
{
    public class RepositoryContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public RepositoryContext(DbContextOptions opt): base(opt)
        {}
    }
}
