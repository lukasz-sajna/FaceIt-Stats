using FaceItStats.Api.Components.Commands;
using FaceItStats.Api.Helpers;
using FaceItStats.Api.Middleware;
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

        public static void UseHangfire(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                Activator = new DependencyJobActivator(serviceScope.ServiceProvider),
                WorkerCount = Environment.ProcessorCount * 5,
                Queues = new[] { Queues.ResetStats}
            });

            GlobalConfiguration.Configuration.UseSerializerSettings(new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.Objects });

            HangFireHelpers.Mediator = serviceScope.ServiceProvider.GetService<IMediator>();

            RecurringJob.AddOrUpdate(() => HangFireHelpers.ResetStats(new ResetStatsCommand()), "0 0 * * *", TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"), Queues.ResetStats);
        }
    }
}
