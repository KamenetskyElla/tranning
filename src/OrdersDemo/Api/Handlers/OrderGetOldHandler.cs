using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;
public record OrderGetOldRequest(int Id) : IRequest<OrderDto>;
public class OrderGetOldHandler(OrderDbContext dbContext, IMapper mapper) : IRequestHandler<OrderGetOldRequest, OrderDto>
{


    public async Task<OrderDto> Handle(OrderGetOldRequest request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        var order = await dbContext.Orders
            .Where(x => x.Id == id)
            .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);
        return order;
    }
}
