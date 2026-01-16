using System.Reflection;
using System.Text.Json;
using WeatherApp.Helpers;
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

  public override Result<Bot> LoadBot(BotConfiguration configuration)
  {
    if (!configuration.Enabled) return Result.Fail<Bot>("Bot not enabled in configuration");

    var botClass = Assembly.GetExecutingAssembly().GetType($"WeatherApp.Weather.Bots.{configuration.Name}");

    if (botClass?.BaseType != typeof(Bot)) return Result.Fail<Bot>($"{configuration.Name} is not a Bot.");
    
    Type[] types = [typeof(BotConfiguration)];
    var botConstructor = botClass.GetConstructor(types);
    Bot newBot = botConstructor?.Invoke([configuration]) as Bot ?? throw new NullReferenceException(message: $"Could not instantiate {configuration.Name}");
    
    return Result.Ok(newBot);
  }

  protected override List<Bot> LoadBotsInternal()
  {
    IEnumerable<BotConfiguration> botConfigurations = GetConfigurations().GetAwaiter().GetResult();

    List<Bot> botList = [];
    foreach (var botConfiguration in botConfigurations)
    {
      var bot = LoadBot(botConfiguration);
      if (bot.IsSuccess)
      {
        botList.Add(bot.Value);
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