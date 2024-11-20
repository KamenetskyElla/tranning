namespace OrdersDemo.Domain.Contracts;

public interface IPriceCalculator
{
    CustomerType Type { get; }

    decimal CalculateDiscount(Order order);
}