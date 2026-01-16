namespace WeatherApp.Weather.Models;

readonly record struct BotConfigurationModel
{
  public decimal? TemperatureThreshold { get; init; }
  public decimal? HumidityThreshold { get; init; }
  public bool Enabled { get; init; }
  public string Message { get; init; }
}