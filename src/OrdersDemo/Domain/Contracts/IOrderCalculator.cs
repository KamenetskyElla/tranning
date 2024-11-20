namespace OrdersDemo.Domain.Contracts;

public interface IOrderCalculator
{
    decimal CalculateDiscount(Order order);
}
