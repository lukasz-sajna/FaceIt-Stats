using FaceItStats.Api.Extensions;
using FaceItStats.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace FaceItStats.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDb(Const.ConnectionString);
            services.AddControllers().AddNewtonsoftJson();
            services.ConfigureSwagger();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy(Const.DefaultCorsPolicy, corsBuilder => corsBuilder
                     .WithOrigins((new List<string> { "http://localhost:4200", "https://faceit-stats-app.azurewebsites.net" }).ToArray())
                     .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
                endpoints.MapHub<FaceItStatsHub>($"{Const.SignalRHubsPathRoot}/faceitStats");
            });
        }
    }
}
