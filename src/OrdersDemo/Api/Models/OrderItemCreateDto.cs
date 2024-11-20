namespace OrdersDemo.Api.Models;

public class OrderItemCreateDto
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
