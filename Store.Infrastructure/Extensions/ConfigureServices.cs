using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ToDO: architectural decisons
namespace Store.Infrastructure.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")
                    ));
            return services;
        }
    }
}
