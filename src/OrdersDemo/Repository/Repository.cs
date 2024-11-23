using Ardalis.Specification.EntityFrameworkCore;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Repository;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public Repository(OrderDbContext dbContext)
        : base(dbContext)
    {
    }
}
