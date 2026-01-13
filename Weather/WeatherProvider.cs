using System.Reflection;
using System.Text.Json;
using WeatherApp.Interfaces;
using WeatherApp.Sources;
using WeatherApp.Weather.Bots;
using WeatherApp.Weather.Factories;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather;

class WeatherManagerProvider : IWeatherManagerProvider
{
  private readonly BotLoader BotLoader = new JSONBotLoader();

  public async Task<List<IBot>> CreateBotsAsync(string configLocation)
  {
    return BotLoader.LoadBots();
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