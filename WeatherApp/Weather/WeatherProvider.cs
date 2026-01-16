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
  private readonly WeatherReaderFactory WeatherSourceFactory = new WeatherReflectionReaderFactory();

// was planing to use async functionality but never came to fruition.
// maybe will work on that later.
  public async Task<List<IBot>> CreateBotsAsync()
  {
    return BotLoader.LoadBots();
  }

  public WeatherManager CreateWeatherManager()
  {
    var manager = new WeatherManager(inputSources: GetWeatherSources());

    foreach (IBot bot in CreateBotsAsync().GetAwaiter().GetResult())
    {
      manager.Subscribe(bot);
    }
    return manager;
  }

  public IWeatherSource[] GetWeatherSources()
  {
    return [.. WeatherSourceFactory.CreateWeatherSources()];
  }

}