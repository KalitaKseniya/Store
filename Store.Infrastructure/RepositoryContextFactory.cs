using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Store.Infrastructure
{
    class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var solutionDir = (Directory.GetCurrentDirectory()).Replace("Store.Infrastructure","Store");//replace store.infrastructure to store ()
            
            var configuration = new ConfigurationBuilder()
               .SetBasePath(solutionDir)
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("sqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
