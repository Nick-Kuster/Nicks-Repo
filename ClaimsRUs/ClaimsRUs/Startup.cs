using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Data.Abstractions.Writers;
using ClaimsRUs.Data.Readers;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Data.Writers;
using ClaimsRUs.Entity;
using ClaimsRUs.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClaimsRUs
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
            services.AddControllersWithViews();
            var connectionString = Configuration.GetConnectionString("cru");


            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            SetUpDI(services);
        }

        private void SetUpDI(IServiceCollection services)
        {
            services.AddTransient<IVehicle, VehicleViewModel>();
            services.AddTransient<IClaim, ClaimViewModel>();
            services.AddTransient<IContact, ContactViewModel>();
            services.AddTransient<IVehiclesReader, VehiclesReader>();
            services.AddTransient<IClaimsReader, ClaimsReader>();
            services.AddTransient<IContactsReader, ContactsReader>();
            services.AddTransient<IClaimsWriter, ClaimsWriter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Claims}/{action=Index}/{id?}");
            });
        }
    }
}
