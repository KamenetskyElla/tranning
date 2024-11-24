using OrderDemo.FunctionalTest.Fixtures;
using OrdersDemo.Infrastructure;

namespace OrderDemo.FunctionalTest.Test;

public class FunctionalTest : IAsyncLifetime
{
    // protected readonly IDateTime _dateTime = new DateTimeBuilder().Build();
    //protected readonly Fixture _fixture = new();

    protected readonly OrderDbContext _dbContext;
    protected readonly Func<Task> _resetDatabase;
    protected readonly HttpClient _client;

    public FunctionalTest(TestFactory testFactory)
    {
        // _dbContext = testFactory.DbContext;
        _resetDatabase = testFactory.ResetDatabase;
        _client = testFactory.HttpClient;
    }

    public Task DisposeAsync()
    {
        _dbContext.ChangeTracker.Clear();
        return _resetDatabase();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public string URLCombine(params dynamic[] uri) => string.Join("/", uri.Select(s => s.ToString().TrimEnd('/')));
}
