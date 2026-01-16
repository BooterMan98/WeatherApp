using WeatherApp.Weather.Models;
using WeatherApp.Weather.Types;

namespace WeatherApp.Weather.Bots;

class RainBot(BotConfiguration configuration) : Bot(configuration)
{

  private Humidity Threshold { get; init; } = configuration.HumidityThreshold ?? throw new InvalidOperationException();
  public override void Analyze(WeatherMeasurement measurement)
  {
    if (measurement.Humidity >= Threshold)
    {
      Console.WriteLine(Message);
    }
  }
}