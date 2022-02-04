namespace Sandogh.Domain.Users
{
    public class AddUserAddressDto
    {
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PostalAddress { get; set; }
        public string ReciverName { get; private set; }
        public string UserId { get; set; }
    }
}
