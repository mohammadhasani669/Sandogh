using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandogh.Domain.Users;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.IdentityConfigs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataBaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataBaseContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<CutomIdentityError>()
                .AddPasswordValidator<CustomPasswordValidator>();

            services.Configure<IdentityOptions>(option =>
            {
                //UserSetting
                //option.User.AllowedUserNameCharacters = "abcd123";
                option.User.RequireUniqueEmail = true;

                //Password Setting
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 8;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
                option.Password.RequireUppercase = false;
                option.Password.RequiredUniqueChars = 1;

                //Lokout Setting
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                //SignIn Setting
                //option.SignIn.RequireConfirmedAccount = false;
                //option.SignIn.RequireConfirmedEmail = true;
                //option.SignIn.RequireConfirmedPhoneNumber = false;

            });

            return services;
        }
    }
}
