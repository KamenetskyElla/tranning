using Ardalis.GuardClauses;

namespace OrdersDemo.Domain;

public class OrderItem
{
    public int Id { get; }

    public string Name { get; } = default!;
    public decimal Price { get; }

    public int OrderId { get; }

    private OrderItem() {}
    public OrderItem(string name, decimal price)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.NegativeOrZero(price);

        Name = name;
        Price = price;
    }
}
