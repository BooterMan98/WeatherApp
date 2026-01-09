using WeatherApp.Weather.Models;

namespace WeatherApp.Interfaces;

/// <summary>
/// Listener of Observer pattern for weather measurements.
/// </summary>
interface IWeatherListener
{
  public void Update(WeatherMeasurement measurement);
}