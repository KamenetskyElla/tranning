using Ardalis.Specification;
using OrdersDemo.Domain;

namespace OrdersDemo.Repository.Spec;

public class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
    }

    public OrderSpec(int id)
    {
        Query.Where(x => x.Id == id);
    }
}

