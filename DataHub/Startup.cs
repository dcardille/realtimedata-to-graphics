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

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder =>
            //        {
            //            builder.AllowAnyMethod()
            //                    .AllowAnyOrigin()
            //                    .AllowAnyHeader()
            //                    .AllowCredentials();
            //        }
            //    );

            //});
                
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7169") // Replace with your client's origin
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5235") // Replace with your client's origin
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


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
            //app.UseCors("CorsPolicy");
            app.UseCors("AllowSpecificOrigin"); // Use the policy name you defined
            app.UseEndpoints(endpoints => { endpoints.MapHub<SpeedHub>("/speedhub"); });
        }
    }
}