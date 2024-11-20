using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Domain.Services;

public class CustomerStandardCalculator : IPriceCalculator
{
    public static readonly CustomerStandardCalculator Instance = new();
    private CustomerStandardCalculator() { }

    public CustomerType Type { get; } = CustomerType.Standard;

    public decimal CalculateDiscount(Order order)
    {
        return order.Total * 0.1m;
    }
}
