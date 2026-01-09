using System.Net.Http.Json;
using System.Text.Json;
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

  public required string JSONContent { get; init;}


  public WeatherMeasurement Read()
  {
    var NotValidatedMeasurement = JsonSerializer.Deserialize<WeatherMeasurementsModel>(JSONContent);
    var measurement = WeatherMeasurementMapper.ToDomain(NotValidatedMeasurement);
  
    return measurement;
  }

}