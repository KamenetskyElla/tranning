using Ardalis.GuardClauses;
using System.IO;

namespace OrdersDemo.Domain;

public class Customer
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Email { get; }
    public CustomerType Type { get; }

    public Customer(CustomerType type, string firstName, string lastName, string? email)
    {
        Guard.Against.NullOrEmpty(firstName);
        Guard.Against.NullOrEmpty(lastName);

        Type = type;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
