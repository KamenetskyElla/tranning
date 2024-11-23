using Ardalis.Specification;

namespace OrdersDemo.Repository;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
