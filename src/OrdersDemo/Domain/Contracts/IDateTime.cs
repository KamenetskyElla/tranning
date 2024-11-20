namespace OrdersDemo.Domain.Contracts;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
