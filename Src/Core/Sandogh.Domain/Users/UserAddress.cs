using Sandogh.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Users
{
    public class UserAddress : Entity
    {
        public string City { get; set; }
        public string State { get;private set; }
        public string ZipCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string UserId { get; private set; }
        public string ReciverName { get; private set; }
        public UserAddress(string city,string state,string zipcode,string postalAddress,string userId,string reciverName)
        {
            City = city;
            State = state;
            ZipCode = zipcode;
            PostalAddress = postalAddress;
            UserId = userId;
            ReciverName = reciverName;
        }
        public UserAddress()
        {

        }
    }
}
