using OrdersDemo.Domain;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Domain.Services;
using OrdersDemo.Infrastructure;
using System.Security.Cryptography;

namespace OrdersDemo.Tests.Fixture.Builders;

public class OrderBuilder
{
    private IOrderCalculator? _calculator;
    private IDateTime? _dateTime;
    private Customer? _customer;
    private Address? _address;
    private string? _orderNo;
    private List<OrderItem> _items = new();
    private decimal? _total;

    public OrderBuilder WithOrderNo(string orderNo)
    {
        _orderNo = orderNo;
        return this;
    }
    public OrderBuilder WithCustomer(Customer customer)
    {
        _customer = customer;
        return this;
    }
    public OrderBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }
    public OrderBuilder WithCustomer(IOrderCalculator orderCalculator)
    {
        _calculator = orderCalculator;
        return this;
    }
    public OrderBuilder WithCustomer(IDateTime dateTime)
    {
        _dateTime = dateTime;
        return this;
    }

    public OrderBuilder WithItems(List<OrderItem> items)
    {
        _items = items;
        return this;
    }

    public OrderBuilder WithTotal(decimal total)
    {
        _total = total;
        return this;
    }

    public Order Build()
    {
        _calculator ??= new OrderCalculator(new List<IPriceCalculator>
        {
            CustomerStandardCalculator.Instance,
            CustomerPremiumCalculator.Instance,
            CustomerVipCalculator.Instance,
            CustomerPresidentCalculator.Instance,
        });

        _dateTime ??= new DateTimeProvider();
        _customer ??= new Customer(CustomerType.Standard, "Firstname", "Lastname", "Email@local.com");
        _address ??= new Address("Street", "City", "PostalCode", "Country");
        _orderNo ??= "000101";

        var order = new Order(_calculator, _dateTime, _orderNo, _customer, _address);

        if (_items.Any())
        {
            _items.ForEach(x => order.AddItem(x));
        }

        if (_total is not null)
        {
            var totalProp = typeof(Order).GetProperty(nameof(Order.Total));
            if (totalProp is not null)
            {
                totalProp.SetValue(order, _total.Value);
            }
        }

        return order;
    }
}
