using FluentAssertions;
using Moq;
using OrdersDemo.Domain;
using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_GivenNullcustomer()
    {
        var dateTime = new Mock<IDateTime>().Object;
        var orderCalculator = new Mock<IOrderCalculator>().Object;
        var orderNo = "000001";

        var action = () => { new Order(orderCalculator, dateTime, orderNo, null!, null!); };

        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_GivenNullOrderCalculator()
    {
        var dateTime = new Mock<IDateTime>().Object;
        var customer = new Customer(CustomerType.Premium, "aa", "aa", "aa");
        var orderNo = "000001";

        var action = () => { new Order(null!, dateTime, orderNo, customer, null!); };

        action.Should().Throw<ArgumentNullException>();
    }
}
