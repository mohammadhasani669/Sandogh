﻿using Microsoft.AspNetCore.Identity;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure.IdentityConfigs
{
    internal class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        //لیست 10.000 پسورد غیر امن رو در لینک زیر میتونید بدست بیارید
        //https://en.wikipedia.org/wiki/Wikipedia:10,000_most_common_passwords


        List<string> CommonPassword = new List<string>()
        {
            "123456","zxcV@34567","password","qwerty","123456789"
        };
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            if (CommonPassword.Contains(password))
            {
                var result = IdentityResult.Failed(new IdentityError
                {
                    Code = "CommonPassword",
                    Description = "پسورد شما قابل شناسایی توسط ربات های هکر است! لطفا یک پسورد قوی انتخاب کنید",
                });
                return Task.FromResult(result);
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
