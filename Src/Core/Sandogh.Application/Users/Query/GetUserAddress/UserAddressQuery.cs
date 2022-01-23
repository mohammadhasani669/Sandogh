using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Users.Query.GetUserAddress
{
    public class UserAddressQuery 
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get;  set; }
        public string ZipCode { get;  set; }
        public string PostalAddress { get;  set; }
        public string ReciverName { get;  set; }
    }
}

