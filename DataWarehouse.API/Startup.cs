using DataWarehouse.API.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using Store.Core.Interfaces;
using Store.LoggerService;
using System.IO;
using DataWarehouse.API.Models;

namespace DataWarehouse.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "/nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddTransient<ProductService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataWarehouse.API", Version = "v1" });
            });
            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProductConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    var rabbitMqConnectionStr = Configuration.GetConnectionString("rabbitMqConnection");
                    cfg.Host(rabbitMqConnectionStr);
                    cfg.ReceiveEndpoint("product-queue", c =>
                    {
                        c.ConfigureConsumer<ProductConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataWarehouse.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
