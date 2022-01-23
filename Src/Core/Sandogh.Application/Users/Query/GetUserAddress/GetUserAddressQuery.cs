using MediatR;
using System.Collections.Generic;

namespace Sandogh.Application.Users.Query.GetUserAddress
{
    public class GetUserAddressQuery : IRequest<List<UserAddressQuery>>
    {
        public string UserId { get; set; }
    }
}

