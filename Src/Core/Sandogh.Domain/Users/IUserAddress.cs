using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Users
{
    public interface IUserAddress : IRepository<UserAddress>
    {
        List<UserAddressDto> GetAddress(string UserId);
        void AddnewAddress(AddUserAddressDto address);
    }
}
