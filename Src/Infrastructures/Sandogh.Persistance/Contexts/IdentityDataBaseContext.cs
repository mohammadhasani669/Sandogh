using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Contexts
{
    public class IdentityDataBaseContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDataBaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
