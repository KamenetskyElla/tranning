using FluentAssertions;
using OrdersDemo.Domain;
using OrdersDemo.Domain.Services;
using OrdersDemo.Tests.Fixture.Builders;

namespace OrdersDemo.Tests.Domain.Services;

public class CustomerPremiumCalculatorTests
{
    [Fact]
    public void CalculateDiscount_Returns15_GivenTotal100()
    {
        var calculator = CustomerPremiumCalculator.Instance;
        var items = new List<OrderItem>
        {
            new OrderItem("Name", 100)
        };

        var order = new OrderBuilder().WithItems(items).Build();

        var result = calculator.CalculateDiscount(order);

        result.Should().Be(15m);
    }

    [Fact]
    public void CalculateDiscount_Returns0_GivenTotal0()
    {
        var calculator = CustomerPremiumCalculator.Instance;

        var order = new OrderBuilder().WithTotal(0).Build();

        var result = calculator.CalculateDiscount(order);

        result.Should().Be(0m);
    }
}
