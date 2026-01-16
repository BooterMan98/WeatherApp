using WeatherApp.Weather.Types;

namespace WeatherApp.Weather.Models;

/// <summary>
/// A weather measurement in a location.
/// </summary>
readonly record struct WeatherMeasurement
{
  required public Location Location { get; init; }
  required public Temperature Temperature { get; init; }
  required public Humidity Humidity { get; init; }
  
}