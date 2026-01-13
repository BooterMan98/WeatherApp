using WeatherApp.Weather.Models;
using WeatherApp.Weather.Types;

namespace WeatherApp.Weather.Bots;

class SnowBot(BotConfiguration configuration) : Bot(configuration)
{

  private Temperature Threshold { get; init; } = configuration.TemperatureThreshold ?? throw new InvalidOperationException();
  public override void Analyze(WeatherMeasurement measurement)
  {
    if (measurement.Temperature <= Threshold)
    {
      Console.WriteLine(Message);
    }
  }
}