using System.Security.Claims;
using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WeatherForecasting;
using WeatherForecasting.CreateWeatherForecast;
using WeatherForecasting.GetWeatherForecast;
using WebApplicationTdd.Controllers;
using WebApplicationTdd.UnitTests.Utilities;
using WebApplicationTdd.UnitTests.Utilities.Attributes;

namespace WebApplicationTdd.UnitTests;

public class WeatherForecastTests
{

    [Theory]
    [InlineAutoNSubstituteData("", "", "", "")]
    [InlineAutoNSubstituteData("8888", "GBP", "010010274414", "52345678901234")]
    public async Task GetWeatherForecast_Allow_ReturnsStatusCode200(
        WeatherForecast weatherForecast,
        CancellationTokenSource cancellationTokenSource,
        [Frozen] IMediator mediator,
        WeatherForecastController sut)
    {
        // arrange
        mediator.Send(Arg.Any<GetWeatherForecastCommand>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(weatherForecast));

        // act
        var actionResult = await sut.Get(cancellationTokenSource.Token);

        // assert
        actionResult.Should().NotBeNull();
        actionResult.Should().BeOfType<ActionResult<IEnumerable<WeatherForecast>>>();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var okResult = actionResult.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.Value.Should().NotBeNull();
        okResult.Value.Should().BeOfType<IEnumerable<WeatherForecast>>();
        var actual = okResult.Value as IEnumerable<WeatherForecast>;
        actual.Should().NotBeNull();
        actual!.First().TemperatureC.Should().Be(15);

        await mediator.Received(1).Send(Arg.Any<GetWeatherForecastCommand>(), Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task CreateWeather_UnauthorizedUser_NoClaims_ReturnsStatusCode403Forbidden(
        WeatherForecast request,
        CancellationTokenSource cancellationTokenSource,
        [Frozen] IMediator mediator,
        WeatherForecastController sut)
    {
        // arrange
        sut.SetupContext();

        // act
        var actionResult = await sut.CreateWeather(request, cancellationTokenSource.Token);

        // assert
        actionResult.Should().NotBeNull();
        actionResult.Should().BeOfType<ForbidResult>();

        await mediator.Received(0).Send(Arg.Any<CreateWeatherForecastCommand>(), Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]

    public async Task CreateWeather_UnauthorizedUser_NotAdmin_ReturnsStatusCode403Forbidden(
        WeatherForecast request,
        CancellationTokenSource cancellationTokenSource,
        [Frozen] IMediator mediator,
        WeatherForecastController sut)
    {
        // arrange
        sut.SetupContext(httpContext =>
        {
            var claimNotAdmin = new Claim("ClaimAmr", string.Empty);

            var identity = Substitute.For<ClaimsIdentity>();
            identity.FindFirst(Arg.Any<string>()).Returns(claimNotAdmin);

            var claimsPrincipal = Substitute.For<ClaimsPrincipal>();
            claimsPrincipal.Identity.Returns(identity);

            httpContext.User.Returns(claimsPrincipal);
        });

        // act
        var actionResult = await sut.CreateWeather(request, cancellationTokenSource.Token);

        // assert
        actionResult.Should().NotBeNull();
        actionResult.Should().BeOfType<ForbidResult>();

        await mediator.Received(0).Send(Arg.Any<CreateWeatherForecastCommand>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineAutoNSubstituteData("", "")]
    [InlineAutoNSubstituteData("AAAA", "")]
    [InlineAutoNSubstituteData("", "BBBB")]
    public async Task CreateWeather_AuthorizedAdminUser_EmptyParams_ReturnsStatusCode400BadRequest(
        string paramA,
        string paramB,
        WeatherForecast request,
        CancellationTokenSource cancellationTokenSource,
        [Frozen] IMediator mediator,
        WeatherForecastController sut)
    {
        // arrange
        sut.SetupContext(SetAdminClaims);

        // act
        var actionResult = await sut.CreateWeather(request, paramA, paramB, cancellationTokenSource.Token);

        // assert
        actionResult.Should().NotBeNull();
        actionResult.Should().BeAssignableTo<IActionResult>();
        actionResult.Should().BeOfType<BadRequestObjectResult>();

        await mediator.Received(0).Send(Arg.Any<CreateWeatherForecastCommand>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineAutoNSubstituteData("AAA1", "BBB3")]
    [InlineAutoNSubstituteData("AAA2", "BBB2")]
    [InlineAutoNSubstituteData("AAA3", "BBB1")]

    public async Task CreateWeather_AuthorizedAdminUser_ReturnsStatusCode200Ok(
        string paramA,
        string paramB,
        WeatherForecast request,
        CancellationTokenSource cancellationTokenSource,
        [Frozen] IMediator mediator,
        WeatherForecastController sut)
    {
        // arrange
        sut.SetupContext(SetAdminClaims);

        // act
        var actionResult = await sut.CreateWeather(request, paramA, paramB, cancellationTokenSource.Token);

        // assert
        actionResult.Should().NotBeNull();
        actionResult.Should().BeAssignableTo<IActionResult>();
        actionResult.Should().BeOfType<OkResult>();

        await mediator.Received(1).Send(Arg.Any<CreateWeatherForecastCommand>(), Arg.Any<CancellationToken>());
    }

    private void SetAdminClaims(HttpContext httpContext)
    {
        var claimAdmin = new Claim("ClaimAmr", "Admin");

        var identity = Substitute.For<ClaimsIdentity>();
        identity.FindFirst("ClaimAmr").Returns(claimAdmin);

        var claimsPrincipal = Substitute.For<ClaimsPrincipal>();
        claimsPrincipal.Identity.Returns(identity);

        httpContext.User.Returns(claimsPrincipal);
    }
}