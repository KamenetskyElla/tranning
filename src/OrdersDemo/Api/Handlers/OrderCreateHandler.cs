﻿using AutoMapper;
using MediatR;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Repository;

namespace OrdersDemo.Api.Handlers;
public record OrderCreateRequest(OrderCreateDto CreateDto) : IRequest<OrderDto>;
public class OrderCreateHandler(IRepository<Order> repository,
                              IMapper mapper,
                              IOrderCalculator orderCalculator,
                              IDateTime dateTime,
                              IDocumentNoGenerator documentNoGenerator) : IRequestHandler<OrderCreateRequest, OrderDto>
{

    public async Task<OrderDto> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
    {
        var createDto = request.CreateDto;
        var customer = new Customer((CustomerType)createDto.Type, createDto.FirstName, createDto.LastName, createDto.Email);
        var address = new Address(createDto.Street, createDto.City, createDto.PostalCode, createDto.Country);
        var orderNo = await documentNoGenerator.GetNewOrderNo();
        var order = new Order(orderCalculator, dateTime, orderNo, customer, address);
        foreach (var itemDto in createDto.Items)
        {
            var item = new OrderItem(itemDto.Name, itemDto.Price);
            order.AddItem(item);
        }
        await repository.AddAsync(order, cancellationToken);
        return mapper.Map<OrderDto>(order);

    }
}
