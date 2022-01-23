using Sandogh.Domain.Users;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Users
{
    public class UserAddressService : EfRepository<UserAddress>, IUserAddress
    {
        public UserAddressService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }

       
    }
}
