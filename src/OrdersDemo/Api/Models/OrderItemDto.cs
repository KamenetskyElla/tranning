namespace OrdersDemo.Api.Models;

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
