using System.Text.Json;
using WeatherApp.Interfaces;
using WeatherApp.Sources.Mappers;
using WeatherApp.Weather.Models;

namespace WeatherApp.Sources;

class JSONWeatherFileSource: IWeatherSourceAsync
{

  public required string SourceFilePath { get; init; }

  public async Task<WeatherMeasurement> ReadAsync()
  {
    var jsonContentStream = File.Open(SourceFilePath, FileMode.Open);

    // TO DO: manage exceptions when file is not valid
    var NotValidatedMeasurement = await JsonSerializer.DeserializeAsync<WeatherMeasurementsModel>(jsonContentStream);
    var measurement = WeatherMeasurementMapper.ToDomain(NotValidatedMeasurement);
  
    return measurement;
  }

}