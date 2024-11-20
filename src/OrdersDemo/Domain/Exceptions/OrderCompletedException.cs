namespace OrdersDemo.Domain.Exceptions;

public class OrderCompletedException : AppException
{
    private const string MESSAGE = "The order is already completed, and can not be modified!";

    public OrderCompletedException() : base(MESSAGE)
    {
    }

    public OrderCompletedException(string? message) : base(message)
    {
    }

    public OrderCompletedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
