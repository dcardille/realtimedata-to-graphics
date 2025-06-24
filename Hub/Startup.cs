using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtimedataToGraphics.Hubs;
using System;

namespace RealtimedataToGraphics
{
  
public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Updated IHostingEnvironment to IWebHostEnvironment
        {

            app.UseExceptionHandler("/Error");
            app.UseHsts();

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            // app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints => { endpoints.MapHub<SpeedHub>("/speedhub"); });
        }
    }
}