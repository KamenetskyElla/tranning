using AutoMapper;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderCreateHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IOrderCalculator _orderCalculator;
    private readonly IDateTime _dateTime;
    private readonly IDocumentNoGenerator _documentNoGenerator;

    public OrderCreateHandler(OrderDbContext dbContext,
                              IMapper mapper,
                              IOrderCalculator orderCalculator,
                              IDateTime dateTime,
                              IDocumentNoGenerator documentNoGenerator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _orderCalculator = orderCalculator;
        _dateTime = dateTime;
        _documentNoGenerator = documentNoGenerator;
    }

    public async Task<OrderDto> HandleAsync(OrderCreateDto createDto, CancellationToken cancellationToken)
    {
        var customer = new Customer((CustomerType)createDto.Type, createDto.FirstName, createDto.LastName, createDto.Email);
        var address = new Address(createDto.Street, createDto.City, createDto.PostalCode, createDto.Country);
        var orderNo = await _documentNoGenerator.GetNewOrderNo();

        var order = new Order(_orderCalculator, _dateTime, orderNo, customer, address);

        foreach (var itemDto in createDto.Items)
        {
            var item = new OrderItem(itemDto.Name, itemDto.Price);
            order.AddItem(item);
        }

        _dbContext.Orders.Add(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
