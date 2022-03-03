using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandogh.Admin.EndPoint.Security.IdentityService;
using Sandogh.Application.BankAccounts.Command.Add;
using Sandogh.Application.Emails;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Application.People.Repository;
using Sandogh.Application.Products.Command.Add;
using Sandogh.Application.Visitors.GetToDayReport;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Orders;
using Sandogh.Domain.Products;
using Sandogh.Domain.Transactions;
using Sandogh.Infrastructure.IdentityConfigs;
using Sandogh.Infrastructure.MappingProfile;
using Sandogh.Persistance.AdminMenus;
using Sandogh.Persistance.Contexts;
using Sandogh.Persistance.Loans;
using Sandogh.Persistance.Orders;
using Sandogh.Persistance.Products;
using Sandogh.Persistance.Transactions;
using System;
using Serilog;
using Sandogh.Admin.EndPoint.Midlewares;

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
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddProductCommand>());

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

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });


            services.AddAuthentication();

            services.AddTransient<IGetTodayReportService, GetTodayReportService>();
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoContext<>));


            services.AddTransient<IPerson, PersonService>();
            services.AddTransient<IBankAccount, BankAccountService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAdminMenu, AdminMenuService>();
            services.AddTransient<ILoan, LoanService>();
            services.AddTransient<IProduct, ProductService>();
            services.AddTransient<IBrand, ProductBrandService>();
            services.AddTransient<IProductCategory, ProductCategoryService>();
            services.AddTransient<IProductFeature, ProductFeatureService>();
            services.AddTransient<IProductImage, ProductImageService>();
            services.AddTransient<ISize, ProductSizeService>();
            services.AddTransient<ITransaction, TransactionService>(); 
            services.AddTransient<IOrder, OrderService>();
            services.AddTransient<IUtilities, Utilities>();


            //Add MediatR
            services.AddMediatR(typeof(AddBankAccountCommand).Assembly);



            //mapper
            services.AddAutoMapper(typeof(BankAccountMappingProfile));



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
            app.UseCustomExceptionHandler();
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 404)
                {
                    context.HttpContext.Response.Redirect("/Error/PageNotFound");
                }
            });
         
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
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "pagination",
                  pattern: "{controller=Home}/{action=Index}/{search}");

               
            });
        }
    }
}
