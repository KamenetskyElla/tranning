using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Domain.Services;

public class CustomerPremiumCalculator : IPriceCalculator
{
    public static readonly CustomerPremiumCalculator Instance = new();
    private CustomerPremiumCalculator() { }

    public CustomerType Type { get; } = CustomerType.Premium;

    public decimal CalculateDiscount(Order order)
    {
        return order.Total * 0.15m;
    }
}
