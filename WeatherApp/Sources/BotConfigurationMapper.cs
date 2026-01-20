using WeatherApp.Weather.Models;
using WeatherApp.Weather.Types;

namespace WeatherApp.Sources;

class BotConfigurationMapper
{
  public static BotConfiguration ToDomain(BotConfigurationModel model, string name)
  {
    return new()
    {
      Name = name,
      Enabled = model.Enabled,
      TemperatureThreshold = model.TemperatureThreshold,
      HumidityThreshold = (Humidity?)model.HumidityThreshold,
      Message = model.Message
    };
  }

  public static (string, BotConfigurationModel) ToModel(BotConfiguration domainModel)
  {
   return (
    domainModel.Name, 
    new()
      {
        Enabled = domainModel.Enabled,
        TemperatureThreshold = domainModel.TemperatureThreshold,
        HumidityThreshold = domainModel.HumidityThreshold,
        Message = domainModel.Message
      }
    ) ;
  }
}