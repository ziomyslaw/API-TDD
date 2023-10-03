using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace WebApplicationTdd.IntegrationTests.Infrastructure;

public class WebApplicationTddFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.integration.json")
                .Build();

            config.AddConfiguration(Configuration);
        });

        // Is be called after the `ConfigureServices` from the Startup
        // which allows you to overwrite the DI with mocked instances
        builder.ConfigureTestServices(services =>
        {
            //commented out code is an example only!
            //MvcServiceCollectionExtensions.AddMvc(services, options => options.Filters.Add(new AllowAnonymousFilter()));
            //services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigMock>();
        });
    }
}
