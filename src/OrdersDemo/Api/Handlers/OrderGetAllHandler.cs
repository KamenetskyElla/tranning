using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrdersDemo.Api.Models;
using OrdersDemo.Infrastructure;

namespace OrdersDemo.Api.Handlers;

public class OrderGetAllHandler
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderGetAllHandler(OrderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<OrderListDto>> HandleAsync(CancellationToken cancellationToken)
    {
        var result = await _dbContext.Orders
            .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
