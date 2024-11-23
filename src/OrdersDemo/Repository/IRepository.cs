using Ardalis.Specification;

namespace OrdersDemo.Repository;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}

