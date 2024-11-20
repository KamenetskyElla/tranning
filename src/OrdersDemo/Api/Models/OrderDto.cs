namespace OrdersDemo.Api.Models;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNo { get; set; } = default!;

    public DateTime DateCreated { get; set; }
    public DateTime? DateCompleted { get; set; }

    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal GrandTotal { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public string Type { get; set; } = default!;

    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    public List<OrderItemDto> Items { get; set; } = new();
}
