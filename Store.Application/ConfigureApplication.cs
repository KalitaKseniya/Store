using Microsoft.Extensions.DependencyInjection;
using Store.Application.Services;
using Store.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
