using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderGetHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderGetHandler(OrderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderDto> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        return order;
    }
}
