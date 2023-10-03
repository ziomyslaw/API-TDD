using Microsoft.Extensions.Configuration;
using Respawn;
using Respawn.Graph;

namespace WebApplicationTdd.IntegrationTests.Infrastructure;

[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<WebApplicationTddFactory>
{
    private readonly RespawnerOptions _checkpoint = new()
    {
        SchemasToInclude = new[]
        {
            ""
        },
        TablesToIgnore = new Table[]
        {
            "sysdiagrams",
            "tblUser",
            "tblObjectType",
            new Table("MyOtherSchema", "MyOtherTable")
        },
        WithReseed = false
    };

    protected readonly WebApplicationTddFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(WebApplicationTddFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();

        const bool isResetDbEnabled = false; // turn off, change this if you need to reset db
        ResetDatabase(_factory.Configuration.GetConnectionString("DB")!, isResetDbEnabled).GetAwaiter().GetResult();
    }

    async Task ResetDatabase(string connection, bool isEnabled)
    {
        if (!isEnabled) return;

        var respawner = await Respawner.CreateAsync(connection, _checkpoint);
        await respawner.ResetAsync(connection);
    }
}
