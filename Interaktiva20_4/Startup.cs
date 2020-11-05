using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interaktiva20_4.Data;
using Interaktiva20_4.Infrastructure;
using Interaktiva20_4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Interaktiva20_4
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".MovieListHolder.Session";
                options.Cookie.IsEssential = true;
            });
            services.AddCors();
            services.AddControllersWithViews();
            services.AddScoped<ICmdbRepository, CmdbRepository>();
            services.AddScoped<IApiClient, ApiClient>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() == false)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder =>
                builder.WithOrigins("https://localhost:44359")
                .AllowAnyHeader()
                 );
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
