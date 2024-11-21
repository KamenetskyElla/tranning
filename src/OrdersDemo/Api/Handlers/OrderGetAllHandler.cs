using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;
public record OrderListRequest() : IRequest<List<OrderListDto>>;
public class OrderGetAllHandler(OrderDbContext dbContext, IMapper mapper) : IRequestHandler<OrderListRequest, List<OrderListDto>>
{
    public async Task<List<OrderListDto>> Handle(OrderListRequest request, CancellationToken cancellationToken)
    {
        var result = await dbContext.Orders
            .ProjectTo<OrderListDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
