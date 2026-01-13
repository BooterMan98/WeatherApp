using System.Reflection;
using System.Text.Json;
using WeatherApp.Interfaces;
using WeatherApp.Sources;
using WeatherApp.Weather.Bots;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather.Factories;

class JSONBotLoader : BotLoader
{
  public string ConfigurationFileLocation { get; init; } = "config.json";
  private readonly JsonSerializerOptions JSONOptions = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

  protected override List<Bot> LoadBotsInternal()
  {
    IEnumerable<BotConfiguration> botConfigurations = GetConfigurations().GetAwaiter().GetResult();

    List<Bot> botList = [];
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
        Bot? newBot = botConstructor?.Invoke([botConfiguration]) as Bot;
        if (newBot is not null)
        {
          botList.Add(newBot);
        }
      }
    }

    return botList;
  }

    private async Task<IEnumerable<BotConfiguration>> GetConfigurations()
  {
    var jsonContentStream = File.Open(ConfigurationFileLocation, FileMode.Open);

    // TO DO: manage exceptions when file is not valid
    var botConfigurationsModel = await JsonSerializer.DeserializeAsync<Dictionary<string, BotConfigurationModel>>(jsonContentStream, JSONOptions) ?? [];

    var botConfigurations = botConfigurationsModel.Select((modelKeyPair) => BotConfigurationMapper.ToDomain(modelKeyPair.Value, modelKeyPair.Key));
    return botConfigurations;
  }

}