using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Common.Services;
using EntityFrameworkCore.Triggered;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStoreSampleApp.Triggered
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
            services.AddRazorPages();
            services.AddTriggeredDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=sample.db");
            });

            services.AddTransient<EmailService>();

            services.AddTransient<IBeforeSaveTrigger<Book>, Triggers.Books.AutoPurchaseFreeBookForAllCustomers>();
            services.AddTransient<IBeforeSaveTrigger<CustomerPurchase>, Triggers.CustomerPurchases.CreateEmail>();
            services.AddTransient<IBeforeSaveTrigger<Customer>, Triggers.Customers.AutoPurchaseFreeBooks>();
            services.AddTransient<IBeforeSaveTrigger<Customer>, Triggers.Customers.SendWelcomeEmail>();
            services.AddTransient<IBeforeSaveTrigger<Customer>, Triggers.Customers.SetSignupDate>();
            services.AddTransient<IAfterSaveTrigger<Email>, Triggers.Emails.SendEmail>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
