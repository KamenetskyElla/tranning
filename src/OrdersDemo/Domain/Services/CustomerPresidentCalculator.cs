using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Domain.Services;

public class CustomerPresidentCalculator : IPriceCalculator
{
    public static readonly CustomerPresidentCalculator Instance = new();
    private CustomerPresidentCalculator() { }

    public CustomerType Type { get; } = CustomerType.President;

    public decimal CalculateDiscount(Order order)
    {
        return order.Total * 0.25m;
    }
}
