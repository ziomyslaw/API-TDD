using MediatR;

namespace WeatherForecasting.GetWeatherForecast;

public class GetWeatherForecastCommandHandler : IRequestHandler<GetWeatherForecastCommand, WeatherForecast>
{
    public async Task<WeatherForecast> Handle(GetWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new WeatherForecast();
    }
}