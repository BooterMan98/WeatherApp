using System.Reflection;
using System.Text.Json;
using WeatherApp.Interfaces;
using WeatherApp.Sources;
using WeatherApp.Weather.Bots;
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
      if (!botConfiguration.Enabled)
      {
        continue;
      }
      var botClass = Assembly.GetExecutingAssembly().GetType($"WeatherApp.Weather.Bots.{botConfiguration.Name}");
      if (botClass?.BaseType == typeof(Bot))
      {
        Type[] types = [typeof(BotConfiguration)];
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

  public WeatherManager CreateWeatherManager(IWeatherSource[] possibleInputSources)
  {
    return new WeatherManager(possibleInputSources);
  }

  public IWeatherSource[] GetWeatherSources()
  {
    var sourceTypes = Assembly.GetExecutingAssembly().GetTypes()
      .Where(type => type.IsAssignableTo(typeof(IWeatherSource)));
    var sourceList = new List<IWeatherSource>();
    foreach (var type in sourceTypes)
    {
      var constructor = type.GetConstructor([]);
      if (constructor is null)
      {
        continue;
      }
      var source = (IWeatherSource)constructor.Invoke([]);
      sourceList.Add(source);
    }
    return [.. sourceList];
  }

}