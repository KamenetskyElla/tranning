using Microsoft.EntityFrameworkCore;
using OrdersDemo.Domain.Contracts;
using OrdersDemo.Infrastructure.Seeds;

namespace OrdersDemo.Infrastructure;

public class OrderDbInitializer
{
    private readonly OrderDbContext _dbContext;

    public OrderDbInitializer(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Seed the Crm test database with predefined values
    /// </summary>
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<OrderDbInitializer>();
            var dateTime = scope.ServiceProvider.GetRequiredService<IDateTime>();

            await initializer._dbContext.Database.EnsureDeletedAsync();
            await initializer._dbContext.Database.EnsureCreatedAsync();

            await initializer.SeedOrders(dateTime);
        }
    }

    public async Task SeedOrders(IDateTime dateTime)
    {
        if (!await _dbContext.Orders.AnyAsync())
        {
            var orders = OrderSeed.GetOrders(dateTime);
            _dbContext.Orders.AddRange(orders);

            await _dbContext.SaveChangesAsync();
        }
    }
}
