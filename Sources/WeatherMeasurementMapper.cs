using WeatherApp.Weather.Models;
using WeatherApp.Weather.Types;

namespace WeatherApp.Sources.Mappers;

static class WeatherMeasurementMapper
{
  public static WeatherMeasurement ToDomain(WeatherMeasurementsModel model)
  {
    return new()
    {
      Location = (Location)model.Location,
      Temperature = model.Temperature,
      Humidity = (Humidity)model.Humidity
    };
  }

  public static WeatherMeasurementsModel ToModel(WeatherMeasurement domainModel)
  {
    return new()
    {
      Location = domainModel.Location,
      Temperature = domainModel.Temperature,
      Humidity = domainModel.Humidity
    };
  }

}
