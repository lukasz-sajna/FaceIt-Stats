using FaceItStats.Api.Helpers;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "FaceIt Stats API v1"); });
        }

        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ServiceCollectionExtensions.MigrateStatisticsDb(serviceScope.ServiceProvider);

            return app;
        }
    }
}
