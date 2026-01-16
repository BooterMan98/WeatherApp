using WeatherApp.Weather.Types;

namespace WeatherApp.Weather.Models;

readonly record struct BotConfiguration
{
  public required string Name { get; init; }
  public required bool Enabled { get; init; }
  public Temperature? TemperatureThreshold 
  { 
    get;
    
    init
    {
      if (value.HasValue)
      {
        field = HumidityThreshold is null ? value : throw new InvalidOperationException(message: errorMessage);
      } else
      {
        field = null;
      }
    } 
    
  }
  public Humidity? HumidityThreshold
  {
    get;
    init
    {
      if (value.HasValue)
      {
        field = TemperatureThreshold is null ? value : throw new InvalidOperationException(message: errorMessage);
      } else
      {
        field = null;
      }
    }
  }
  public required string Message { get; init; }

  private readonly string errorMessage = "A bot can't have more than one sensor threshold.";

  public BotConfiguration()
  {
  }
}