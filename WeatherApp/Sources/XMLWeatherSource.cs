using System.Xml.Schema;
using System.Xml.Serialization;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Sources.Mappers;
using WeatherApp.Weather.Models;

namespace WeatherApp.Sources;

class XMLWeatherSource : IWeatherSource
{
  public bool IsReadable(string data)
  {
     return data.StartsWith("<");
  }

  public Result<WeatherMeasurement> Read(string data)
  {

    WeatherMeasurement measurement;
    try
    {
      using (TextReader reader = new StringReader(data))
      {
      var serializer = new XmlSerializer(typeof(WeatherMeasurementsModel));

      var deserializedMeasurement = serializer.Deserialize(reader) ?? throw new NullReferenceException();
      var NotValidatedMeasurement = (WeatherMeasurementsModel)deserializedMeasurement;

      measurement = WeatherMeasurementMapper.ToDomain(NotValidatedMeasurement);
      }
    } catch ( InvalidOperationException e)
    {
      
      return Result.Fail<WeatherMeasurement>(e.InnerException?.Message ?? e.Message);
    }
    return Result.Ok(measurement);
  }
}