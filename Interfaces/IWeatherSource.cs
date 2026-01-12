using WeatherApp.Helpers;
using WeatherApp.Weather.Models;

namespace WeatherApp.Interfaces;

 
interface IWeatherSource
{
  Result<WeatherMeasurement> Read(string data);

/// <summary>
/// Checks if data given follows the data structure of the source at the beginning of the string.
/// </summary>
/// <param name="data"></param>
/// <returns></returns>
  Boolean IsReadable(string data);
}

interface IWeatherSourceAsync {
  Task<WeatherMeasurement> ReadAsync();

}