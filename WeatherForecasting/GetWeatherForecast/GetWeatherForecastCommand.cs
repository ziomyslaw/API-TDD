using MediatR;

namespace WeatherForecasting.GetWeatherForecast;

public record GetWeatherForecastCommand() : IRequest<WeatherForecast>;
