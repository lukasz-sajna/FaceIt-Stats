using FaceItStats.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace FaceItStats.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FaceIt Stats API",
                    Version = "v1"
                });
            });

            return services;
        }

        public static void AddDb(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<FaceitDbContext>(options => options.UseSqlite(connectionString));
        }

        public static void MigrateStatisticsDb(IServiceProvider serviceProvider)
        {
            using var dbContext = serviceProvider.GetService<FaceitDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
