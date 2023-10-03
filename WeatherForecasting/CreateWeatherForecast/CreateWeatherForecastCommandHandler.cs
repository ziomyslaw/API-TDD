using MediatR;
using WeatherForecasting.CreateWeatherForecast;

namespace WeatherForecasting;

public class CreateWeatherForecastCommandHandler : IRequestHandler<CreateWeatherForecastCommand>
{
    public async Task Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        //await dbContext.SaveChangesAsync();
        await Task.CompletedTask;

    }
}
