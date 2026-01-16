using WeatherApp.Interfaces;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather.Bots;

abstract class Bot(BotConfiguration configuration) : IBot
{
  public bool Enabled { get; init; } = configuration.Enabled;
  public string Message { get; init; } = configuration.Message;

  abstract public void Analyze(WeatherMeasurement measurement);

  public void Update(WeatherMeasurement measurement)
  {
    Analyze(measurement);
  }
}