using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;
public record OrderUpdateRequest(int Id, OrderUpdateDto UpdateDto) : IRequest<OrderDto>;

public class OrderUpdateHandler(OrderDbContext dbContext, IMapper mapper) : IRequestHandler<OrderUpdateRequest, OrderDto>
{
    public async Task<OrderDto> Handle(OrderUpdateRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        OrderUpdateDto updateDto = request.UpdateDto;
        var order = await dbContext.Orders
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(id, order);

        var address = new Address(updateDto.Street, updateDto.City, updateDto.PostalCode, updateDto.Country);
        order.UpdateAddress(address);

        //_dbContext.Orders.Update(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<OrderDto>(order);
    }
}
