using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderDeleteHandler
{
    private readonly OrderDbContext _dbContext;

    public OrderDeleteHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        _dbContext.Orders.Remove(order);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
