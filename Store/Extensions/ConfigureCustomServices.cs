using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Application.Services;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Infrastructure;
using Store.Infrastructure.Repositories;
using System;
using System.Net;
using System.Text;

namespace Store.Extensions
{
    public static class ConfigureCustomServices
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app,
            ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
        public static void ConfigureSwaggerVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        public static void ConfigureCurrencyService(this IServiceCollection services)
            => services.AddScoped<ICurrencyService, CurrencyService>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 4;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetSection("key").Value + jwtSettings.GetSection("key").Value;

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

        }

        public static void ConfigureAuthentication(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationManager, Application.Services.AuthenticationManager>();

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()//.WithOrigins(configuration.GetSection("ClientUrl").Value)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("pagination"));
            });
        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
            =>  services.AddScoped<ILoggerManager, LoggerManager>();

        public static IServiceCollection ConfigureDI(this IServiceCollection services, IConfiguration configuration)
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

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                });
            });

            services.AddMassTransitHostedService();
            //var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            //{
            //    config.Host("amqp://guest:guest@localhost:5672");

            //    config.ReceiveEndpoint("temp-queue", c =>
            //    {
            //        c.Handler<Product>(ctx =>
            //        {
            //            return Console.Out.WriteLineAsync(ctx.Message.Name);
            //        });
            //    });
            //});

            //bus.Start();
            //bus.Publish(new Product { Name = "test name" });
            return services;
        }

    }
}


