using OrdersDemo.Domain;

namespace OrdersDemo.Tests.Fixture.Builders;

public class OrderItemBuilder
{
    private decimal? _price;

    public OrderItemBuilder WithPrice(decimal? price)
    {
        _price = price;
        return this;
    }

    public OrderItem Build()
    {
        _price ??= 100m;
        return new OrderItem("Name", _price.Value);
    }
}
