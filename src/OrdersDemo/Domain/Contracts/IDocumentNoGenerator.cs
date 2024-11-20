namespace OrdersDemo.Domain.Contracts;

public interface IDocumentNoGenerator
{
    Task<string> GetNewOrderNo();
}
