using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Infrastructure;

public class DateTimeProvider : IDateTime
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
