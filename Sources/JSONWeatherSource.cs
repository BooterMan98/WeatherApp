using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Sources.Mappers;
using WeatherApp.Weather.Models;

namespace WeatherApp.Sources;

/// <summary>
/// A reader of JSON objects for <seealso cref="WeatherMeasurement"/>
/// </summary>
/// 
class JSONWeatherSource : IWeatherSource
{

  public bool IsReadable(string data)
  {
    return data.StartsWith("{");
  }

  public Result<WeatherMeasurement> Read(string data)
  {
    try
    {
      var NotValidatedMeasurement = JsonSerializer.Deserialize<WeatherMeasurementsModel>(data);
      var measurement = WeatherMeasurementMapper.ToDomain(NotValidatedMeasurement);
      return Result.Ok(measurement);
    } catch (Exception e)
    {
      return Result.Fail<WeatherMeasurement>(e.Message);
    }
  
  }

}