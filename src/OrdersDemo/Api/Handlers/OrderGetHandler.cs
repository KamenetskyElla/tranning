using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Repository;

namespace OrdersDemo.Api.Handlers;
public record OrderGetRequest(int Id) : IRequest<OrderDto>;
public class OrderGetHandler(IReadRepository<Order> readRepository, IMapper mapper) : IRequestHandler<OrderGetRequest, OrderDto>
{
    public async Task<OrderDto> Handle(OrderGetRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var order = await readRepository.GetByIdAsync(id, cancellationToken);
        Guard.Against.NotFound(id, order);
        return mapper.Map<OrderDto>(order);
    }
}
