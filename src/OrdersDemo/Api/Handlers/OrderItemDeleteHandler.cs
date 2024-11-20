using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderItemDeleteHandler
{
    private readonly OrderDbContext _dbContext;

    public OrderItemDeleteHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(int id, int itemId, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        order.DeleteItem(itemId);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
