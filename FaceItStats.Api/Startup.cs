using FaceItStats.Api.Configs;
using FaceItStats.Api.Extensions;
using FaceItStats.Api.Hubs;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace FaceItStats.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDb(Const.ConnectionString);
            services.AddControllers().AddNewtonsoftJson(options =>
             options.UseCamelCasing(true)
             );
            services.ConfigureSwagger();
            services.ConfigureSignalR();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddCors(options =>
            {
                options.AddPolicy(Const.DefaultCorsPolicy, corsBuilder => corsBuilder
                     .WithOrigins((new List<string> { "http://localhost:4200", "https://faceit-stats-app.azurewebsites.net" }).ToArray())
                     .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
            });
            services.Configure<ExcludedCompetitions>(Configuration.GetSection(nameof(ExcludedCompetitions)));
            services.AddHangfire(globalConfiguration =>
                {
                    globalConfiguration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
                    globalConfiguration.UseSimpleAssemblyNameTypeSerializer();
                    globalConfiguration.UseRecommendedSerializerSettings();
                    globalConfiguration.UseSQLiteStorage(Const.HangfireConnectionString);
                    globalConfiguration.UseMediatR();
                }
            );

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.Configure<Auth>(Configuration.GetSection(nameof(Auth)));
            services.Configure<ThirdPartyApis>(Configuration.GetSection(nameof(ThirdPartyApis)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard(options: new DashboardOptions { StatsPollingInterval = 5000 });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                // Read and use headers coming from reverse proxy: X-Forwarded-For X-Forwarded-Proto
                // This is particularly important so that HttpContent.Request.Scheme will be correct behind a SSL terminating proxy
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.UseCors(Const.DefaultCorsPolicy);
            app.UpdateDatabase();
            app.UseCustomSwagger();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
                endpoints.MapHub<FaceItStatsHub>($"{Const.SignalRHubsPathRoot}/faceitStats");
                endpoints.MapHub<NotificationsHub>($"{Const.SignalRHubsPathRoot}/notifications");
                endpoints.MapHub<ChallengeHub>($"{Const.SignalRHubsPathRoot}/challengeStats");
            });
        }
    }
}
