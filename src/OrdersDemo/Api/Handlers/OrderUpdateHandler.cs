using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderUpdateHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderUpdateHandler(OrderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderDto> HandleAsync(int id, OrderUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        var address = new Address(updateDto.Street, updateDto.City, updateDto.PostalCode, updateDto.Country);
        order.UpdateAddress(address);

        //_dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
