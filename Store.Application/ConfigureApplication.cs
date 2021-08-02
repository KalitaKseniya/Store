using Microsoft.Extensions.DependencyInjection;
using Store.Application.Services;
using Store.Core.Interfaces;

namespace Store.Application
{
    public static class ConfigureApplication
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            return services;
        }
    }
}
