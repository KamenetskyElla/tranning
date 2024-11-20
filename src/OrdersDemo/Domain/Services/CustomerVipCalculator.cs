using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Domain.Services;

public class CustomerVipCalculator : IPriceCalculator
{
    public static readonly CustomerVipCalculator Instance = new();
    private CustomerVipCalculator() { }

    public CustomerType Type { get; } = CustomerType.Vip;

    public decimal CalculateDiscount(Order order)
    {
        return order.Total * 0.2m;
    }
}
