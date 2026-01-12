using System.Reflection;
using System.Text.Json;
using WeatherApp.Interfaces;
using WeatherApp.Sources;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather;

class WeatherManagerProvider : IWeatherManagerProvider
{
  public async Task<List<IBot>> CreateBotsAsync(string configLocation)
  {
    IEnumerable<BotConfiguration> botConfigurations = await GetConfigurationsFromFile(configLocation);

    List<IBot> botList = [];
    foreach (var botConfiguration in botConfigurations)
    {
      var botClass = Assembly.GetExecutingAssembly().GetType($"WeatherApp.Weather.Bots.{botConfiguration.Name}");
      if (botClass?.IsAssignableTo(typeof(IBot)) ?? false)
      {
        var types = new Type[1];
        types[0] = typeof(BotConfiguration);
        var botConstructor = botClass.GetConstructor(types);
        IBot? newBot = botConstructor?.Invoke([botConfiguration]) as IBot;
        if (newBot is not null)
        {
          botList.Add(newBot);
        }
      }
    }

    return botList;
  }

  private static async Task<IEnumerable<BotConfiguration>> GetConfigurationsFromFile(string configLocation)
  {
    var jsonContentStream = File.Open(configLocation, FileMode.Open);

    var serializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    // TO DO: manage exceptions when file is not valid
    var botConfigurationsModel = await JsonSerializer.DeserializeAsync<Dictionary<string, BotConfigurationModel>>(jsonContentStream, serializerOptions) ?? [];


    var botConfigurations = botConfigurationsModel.Select((modelKeyPair) => BotConfigurationMapper.ToDomain(modelKeyPair.Value, modelKeyPair.Key));
    return botConfigurations;
  }

  public WeatherManager CreateWeatherManager()
  {
    return new WeatherManager();
  }


}