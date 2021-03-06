namespace Sandogh.Domain.Orders
{
    public class Address
    {
        public string State { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string ReciverName { get; private set; }
        public Address(string city, string state, string zipCode, string postalAddress)
        {
            this.City = city;
            State = state;
            ZipCode = zipCode;
            PostalAddress = postalAddress;
        }
    }
}
