using Ardalis.Specification.EntityFrameworkCore;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Repository;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public ReadRepository(OrderDbContext dbContext)
        : base(dbContext)
    {
    }
}
