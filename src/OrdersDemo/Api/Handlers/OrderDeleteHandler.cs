using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public record OrderDeleteRequest(int Id) : IRequest<Unit>;

public class OrderDeleteHandler(OrderDbContext dbContext) : IRequestHandler<OrderDeleteRequest, Unit>
{

    public async Task<Unit> Handle(OrderDeleteRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var order = await dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        dbContext.Orders.Remove(order);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
