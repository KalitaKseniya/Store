using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Store.Infrastructure
{
    class RepositoryContextFactory: IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("sqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }  
}
