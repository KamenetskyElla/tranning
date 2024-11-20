using OrdersDemo.Domain;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Domain.Services;

namespace OrdersDemo.Infrastructure.Seeds;

public static class OrderSeed
{
    public static List<Order> GetOrders(IDateTime dateTimeService)
    {
        var orders = new List<Order>
        {
            GenerateOrder(1, 1.ToString("D6"), dateTimeService),
            GenerateOrder(2, 2.ToString("D6"), dateTimeService),
            GenerateOrder(3, 3.ToString("D6"), dateTimeService),
            GenerateOrder(4, 4.ToString("D6"), dateTimeService),
            GenerateOrder(5, 5.ToString("D6"), dateTimeService)
        };

        return orders;
    }

    public static Order GenerateOrder(int index, string orderNo, IDateTime dateTimeService)
    {
        var customer = new Customer(CustomerType.Standard, $"FirstName{index}", $"LastName{index}", $"person{index}@local.com");
        var address = new Address($"Street {index}", $"City{index}", $"1{index:D4}", $"Country{index}");

        var order = new Order(OrderCalculator.Instance, dateTimeService, orderNo, customer, address);

        order.AddItem(new OrderItem($"item{index}-1", 50m));
        order.AddItem(new OrderItem($"item{index}-2", 70m));
        order.AddItem(new OrderItem($"item{index}-3", 100m));

        return order;
    }
}
