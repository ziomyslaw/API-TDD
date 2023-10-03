using MediatR;

namespace WeatherForecasting.CreateWeatherForecast;

public record CreateWeatherForecastCommand(WeatherForecast WeatherForecast) : IRequest;
