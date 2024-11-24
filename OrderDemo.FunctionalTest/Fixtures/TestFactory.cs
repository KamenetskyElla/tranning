using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using OrderDemo.FunctionalTest.Builder;
using System.Data.Common;

namespace OrderDemo.FunctionalTest.Fixtures;

public class TestFactory : WebApplicationFactory<ClaimsMarker>, IAsyncLifetime
{
    private DbConnection _dbConnection = default!;
    // private Respawner _respawner = default!;

    // public ClaimsDbContext DbContext { get; set; } = default!;
    public HttpClient HttpClient { get; set; } = default!;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentExtensions.Testing);
        builder.UseContentRoot(Path.Combine(Environment.CurrentDirectory, "../../../../../../"));
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
    }

    public async Task ResetDatabase()
    {
        // await _respawner.ResetAsync(_dbConnection);
    }

    public async Task InitializeAsync()
    {
        HttpClient = CreateClient();

        //DbContext = Services.CreateScope().ServiceProvider.GetRequiredService<ClaimsDbContext>();
        //await DbContext.Database.EnsureDeletedAsync();
        //await DbContext.Database.EnsureCreatedAsync();

        //_dbConnection = DbContext.Database.GetDbConnection();
        //await _dbConnection.OpenAsync();

        //_respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        //{
        //    DbAdapter = DbAdapter.SqlServer,
        //    SchemasToInclude = new[] { "dbo" },
        //    CheckTemporalTables = true
        //});
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        //  await _dbConnection.CloseAsync();
    }
}
