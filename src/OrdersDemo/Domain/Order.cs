using Ardalis.GuardClauses;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Domain.Exceptions;
using OrdersDemo.Domain.Services;

namespace OrdersDemo.Domain;

public class Order
{
    private readonly IOrderCalculator _orderCalculator;

    public int Id { get; }

    public string OrderNo { get; } = default!;

    public Customer Customer { get; } = default!;
    public Address? Address { get; private set; }

    public DateTime DateCreated { get; }
    public DateTime? DateCompleted { get; private set; }

    public decimal Total { get; private set; }
    public decimal Discount { get; private set; }
    public decimal GrandTotal { get; private set; }

    public bool IsCompleted => DateCompleted != null;

    private readonly List<OrderItem> _items = new();

    public IEnumerable<OrderItem> Items => _items.AsEnumerable();

    private Order()
    {
        _orderCalculator = OrderCalculator.Instance;
    }

    public Order(IOrderCalculator orderCalculator, IDateTime dateTime, string orderNo, Customer customer, Address address)
    {
        if (string.IsNullOrEmpty(orderNo))
        {
            throw new ArgumentNullException();
        }
        Guard.Against.NullOrEmpty(orderNo);
        Guard.Against.Null(orderCalculator);
        Guard.Against.Null(dateTime);
        Guard.Against.Null(customer);

        _orderCalculator = orderCalculator;
        OrderNo = orderNo;
        Customer = customer;
        Address = address;

        DateCreated = dateTime.Now;
    }

    public void UpdateAddress(Address address)
    {
        Guard.Against.Null(address);

        if (IsCompleted) throw new OrderCompletedException();

        Address = address;
    }

    public void Complete(IDateTime dateTime)
    {
        Guard.Against.Null(dateTime);

        DateCompleted = dateTime.Now;
    }

    public OrderItem AddItem(OrderItem item)
    {
        Guard.Against.Null(item);

        if (IsCompleted) throw new OrderCompletedException();

        _items.Add(item);

        CalculateTotal();

        return item;
    }

    public void DeleteItem(int itemId)
    {
        if (IsCompleted) throw new OrderCompletedException();

        var orderItem = _items.FirstOrDefault(x => x.Id == itemId);

        Guard.Against.NotFound(itemId, orderItem);

        _items.Remove(orderItem);

        CalculateTotal();
    }

    private void CalculateTotal()
    {
        Total = Items.Sum(x => x.Price);
        Discount = _orderCalculator.CalculateDiscount(this);
        GrandTotal = Total - Discount;
    }
}
