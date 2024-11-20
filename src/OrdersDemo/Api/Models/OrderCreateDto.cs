namespace OrdersDemo.Api.Models;

public class OrderCreateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public int Type { get; set; }

    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    public List<OrderItemCreateDto> Items { get; set; } = new();
}
