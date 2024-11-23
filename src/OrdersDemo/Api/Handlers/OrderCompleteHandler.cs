using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderCompleteHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IDateTime _dateTime;

    public OrderCompleteHandler(OrderDbContext dbContext, IMapper mapper, IDateTime dateTime)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dateTime = dateTime;
    }

    public async Task<OrderDto> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        order.Complete(_dateTime);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
