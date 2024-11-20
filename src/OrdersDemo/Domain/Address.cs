using Ardalis.GuardClauses;

namespace OrdersDemo.Domain;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string PostalCode { get; }
    public string Country { get; }

    public Address(string street, string city, string postalCode, string country)
    {
        Guard.Against.NullOrEmpty(street);
        Guard.Against.NullOrEmpty(city);
        Guard.Against.NullOrEmpty(postalCode);
        Guard.Against.NullOrEmpty(country);

        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }
}
