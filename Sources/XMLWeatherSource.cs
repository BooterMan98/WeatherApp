using System.Xml.Schema;
using System.Xml.Serialization;
using WeatherApp.Interfaces;
using WeatherApp.Sources.Mappers;
using WeatherApp.Weather.Models;

namespace WeatherApp.Sources;

class XMLWeatherSource : IWeatherSource
{

  public required string XMLContent { get; init; }

  public WeatherMeasurement Read()
  {

    WeatherMeasurement measurement;
    using (TextReader reader = new StringReader(XMLContent))
    {
    var serializer = new XmlSerializer(typeof(WeatherMeasurementsModel));

    var deserializedMeasurement = serializer.Deserialize(reader) ?? throw new NullReferenceException();
    var NotValidatedMeasurement = (WeatherMeasurementsModel)deserializedMeasurement;

    measurement = WeatherMeasurementMapper.ToDomain(NotValidatedMeasurement);
    }
    return measurement;
  }
}