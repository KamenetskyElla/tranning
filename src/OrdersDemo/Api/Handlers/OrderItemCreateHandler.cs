using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderItemCreateHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderItemCreateHandler(OrderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderDto> HandleAsync(int id, OrderItemCreateDto createDto, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        var item = new OrderItem(createDto.Name, createDto.Price);
        order.AddItem(item);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
