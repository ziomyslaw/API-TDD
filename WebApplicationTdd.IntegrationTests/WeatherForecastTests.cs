using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using WebApplicationTdd.IntegrationTests.Infrastructure;
using WeatherForecasting;

namespace WebApplicationTdd.IntegrationTests;

public class WeatherForecastTests : IntegrationTest
{
    public WeatherForecastTests(WebApplicationTddFactory application) : base(application) { }

    [Fact]
    public void WeatherForecast()
    {
    }

    [Theory]
    [InlineData(0, 15, "Warm")]
    [InlineData(1, 15, "Warm")]
    [InlineData(2, 15, "Warm")]
    [InlineData(30, 15, "Warm")]
    public async Task POST_WeatherForecast_Create_Unauthorized(int daysOffset, int temperatureC, string? summary)
    {
        var response = await POST_WeatherForecast_Create(daysOffset, temperatureC, summary);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData(0, 15, "Warm")]
    [InlineData(1, 15, "Warm")]
    [InlineData(2, 15, "Warm")]
    [InlineData(30, 15, "Warm")]
    public async Task POST_WeatherForecast_Create_NotAdmin_Forbidden(int daysOffset, int temperatureC, string? summary)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {JtwToken.NotAdmin}");
        var response = await POST_WeatherForecast_Create(daysOffset, temperatureC, summary);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Theory]
    [InlineData(0, 15, "Warm")]
    [InlineData(1, 15, "Warm")]
    [InlineData(2, 15, "Warm")]
    [InlineData(30, 15, "Warm")]
    public async Task POST_WeatherForecast_Create_Admin_Ok(int daysOffset, int temperatureC, string? summary)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {JtwToken.Admin}");
        var response = await POST_WeatherForecast_Create(daysOffset, temperatureC, summary);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task<HttpResponseMessage> POST_WeatherForecast_Create(int daysOffset, int temperatureC, string? summary)
    {
        WeatherForecast request = new()
        {
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(daysOffset)),
            TemperatureC = temperatureC,
            Summary = summary
        };
        HttpContent content = JsonContent.Create(request);
        return await _client.PostAsync("api/weatherForecast", content);
    }
}