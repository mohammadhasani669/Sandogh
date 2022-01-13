using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandogh.Application.BankAccounts.Command.Add;
using Sandogh.Application.BankAccounts.Repository;
using Sandogh.Application.Emails;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Application.People.Repository;
using Sandogh.Application.Visitors.GetToDayReport;
using Sandogh.Infrastructure.IdentityConfigs;
using Sandogh.Infrastructure.MappingProfile;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint
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

            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddTransient<IDataBaseContext, DatabaseContext>();
            services.AddIdentityService(Configuration);
            services.AddAuthorization();
            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                option.LoginPath = "/Account/login";
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.SlidingExpiration = true;
            });
            //Add MediatR
            services.AddMediatR(typeof(AddBankAccountCommand).Assembly);

            services.AddAutoMapper(typeof(BankAccountMappingProfile));

            services.AddAuthentication();

            services.AddTransient<IGetTodayReportService, GetTodayReportService>();
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoContext<>));
            

            services.AddTransient<IPerson, PersonService>();
            services.AddTransient<IBankAccount, BankAccountService>();
            services.AddTransient<IEmailService, EmailService>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "pagination",
                    pattern: "{controller=Home}/{action=Index}/{search}/Page{PageNumber}");

                endpoints.MapControllerRoute(
                    name: "pagination",
                    pattern: "{controller=Home}/{action=Index}/Page{PageNumber}");

                endpoints.MapControllerRoute(
                  name: "pagination",
                  pattern: "{controller=Home}/{action=Index}/{search}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
