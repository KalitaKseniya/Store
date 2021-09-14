using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Interfaces;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")
                    ));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            return services;
        }
    }
}
