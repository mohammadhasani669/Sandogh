using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandogh.Application.Emails;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Application.People.Repository;
using Sandogh.Application.Products.Command.Add;
using Sandogh.Application.Products.Queries.GetAll;
using Sandogh.Application.Visitors.SaveVisitorInfo;
using Sandogh.Common;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Products;
using Sandogh.Domain.Transactions;
using Sandogh.Infrastructure.IdentityConfigs;
using Sandogh.Infrastructure.MappingProfile;
using Sandogh.Persistance.AdminMenus;
using Sandogh.Persistance.Carts;
using Sandogh.Persistance.Contexts;
using Sandogh.Persistance.Loans;
using Sandogh.Persistance.Products;
using Sandogh.Persistance.Transactions;
using Sandogh.WebSite.EndPoint.Hubs;
using Sandogh.WebSite.EndPoint.Utilities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandogh.WebSite.EndPoint
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
            Configs configs = new Configs();


            configs.EmailPassword = Configuration[nameof(configs.EmailPassword)];
            configs.GoogleAuthenticationClientId = Configuration[nameof(configs.GoogleAuthenticationClientId)];
            configs.GoogleAuthenticationClientSecret = Configuration[nameof(configs.GoogleAuthenticationClientSecret)];
            configs.GoogleRecaptchaSecretKey = Configuration[nameof(configs.GoogleRecaptchaSecretKey)];
            configs.GoogleRecaptchaSitekey = Configuration[nameof(configs.GoogleRecaptchaSitekey)];

            configs.DapperConnetionString = Configuration.GetConnectionString("SqlServer");

            services.AddSingleton(configs);

            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddProductCommand>());

            services.AddTransient<IDataBaseContext, DatabaseContext>();
            //services.AddTransient<IIdentityDatabaseContext, IdentityDatabaseContext>();
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddIdentityService(Configuration);
            services.AddAuthorization();
            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                option.LoginPath = "/Account/login";
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.SlidingExpiration = true;
            });

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "1";// configs.GoogleAuthenticationClientId;
                options.ClientSecret = "1";//configs.GoogleAuthenticationClientSecret;
            });

            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoContext<>));
            services.AddTransient<SaveVisitorInfoService, SaveVisitorInfoService>();
            services.AddScoped<SaveVisitorFilter>();


            services.AddTransient<ICart, CartService>();
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


            //Add MediatR
            services.AddMediatR(typeof(GetAllProductQuery).Assembly);

            //mapper
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddSignalR();

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
                //باید بالاترین باشه تا  مشکل ایجاد نکنه
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<OnlineVisitorHub>("/chathub");
            });
        }
    }
}
