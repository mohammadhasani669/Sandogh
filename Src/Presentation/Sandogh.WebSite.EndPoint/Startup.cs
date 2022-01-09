using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandogh.Application.Emails;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Application.Visitors.SaveVisitorInfo;
using Sandogh.Infrastructure;
using Sandogh.Infrastructure.IdentityConfigs;
using Sandogh.Persistance.Contexts;
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
          
           
            configs.EmailPassword = Configuration[nameof(configs.EmailPassword) ];
            configs.GoogleAuthenticationClientId = Configuration[nameof(configs.GoogleAuthenticationClientId)];
            configs.GoogleAuthenticationClientSecret = Configuration[nameof(configs.GoogleAuthenticationClientSecret)];
            configs.GoogleRecaptchaSecretKey = Configuration[nameof(configs.GoogleRecaptchaSecretKey)];
            configs.GoogleRecaptchaSitekey = Configuration[nameof(configs.GoogleRecaptchaSitekey)];

            configs.DapperConnetionString = Configuration.GetConnectionString("SqlServer");

            services.AddSingleton(configs);
            services.AddControllersWithViews();
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
                options.ClientId = configs.GoogleAuthenticationClientId;
                options.ClientSecret = configs.GoogleAuthenticationClientSecret;
            });

            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoContext<>));
            services.AddTransient<SaveVisitorInfoService,SaveVisitorInfoService>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<SaveVisitorFilter>();


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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<OnlineVisitorHub>("/chathub");
            });
        }
    }
}
