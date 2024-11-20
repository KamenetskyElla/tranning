using Microsoft.EntityFrameworkCore;
using OrdersDemo.Domain.Contracts;

namespace OrdersDemo.Infrastructure;

public class DocumentNoGenerator : IDocumentNoGenerator
{
    private readonly OrderDbContext _dbContext;
    private readonly ILogger<DocumentNoGenerator> _logger;

    public DocumentNoGenerator(OrderDbContext dbContext,
                               ILogger<DocumentNoGenerator> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> GetNewOrderNo()
    {
        var lastNo = await _dbContext.Orders
                    .Select(x => x.OrderNo)
                    .OrderByDescending(x => x.Length)
                    .ThenByDescending(x => x)
                    .FirstOrDefaultAsync();

        _logger.LogInformation("The last found order no is: {lastNo}", lastNo);
        return (Convert.ToInt32(lastNo) + 1).ToString("D6");
    }
}

