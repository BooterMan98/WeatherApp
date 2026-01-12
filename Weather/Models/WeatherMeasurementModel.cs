using System.Xml.Serialization;
using WeatherApp.Weather.Types;

namespace WeatherApp.Weather.Models;

/// <summary>
/// A version of the <seealso cref="WeatherMeasurement"/> class represented by primitives
/// </summary>
/// <remarks>
/// To be used when reading from a source with XML or JSON content.
/// </remarks>
[XmlRoot("WeatherData")]
public readonly record struct WeatherMeasurementsModel
{
  required public string Location { get; init; }
  required public decimal Temperature { get; init; }
  required public decimal Humidity { get; init; }
  
}