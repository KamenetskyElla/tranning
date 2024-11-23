using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Repository;

namespace OrdersDemo.Api.Handlers;
public record OrderUpdateRequest(int Id, OrderUpdateDto UpdateDto) : IRequest<OrderDto>;

public class OrderUpdateHandler(IRepository<Order> repository, IMapper mapper) : IRequestHandler<OrderUpdateRequest, OrderDto>
{
    public async Task<OrderDto> Handle(OrderUpdateRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        OrderUpdateDto updateDto = request.UpdateDto;
        var order = await repository.GetByIdAsync(id, cancellationToken);
        Guard.Against.NotFound(id, order);
        var address = new Address(updateDto.Street, updateDto.City, updateDto.PostalCode, updateDto.Country);
        order.UpdateAddress(address);
        await repository.UpdateAsync(order, cancellationToken);
        return mapper.Map<OrderDto>(order);
    }
}
