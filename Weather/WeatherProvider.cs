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
    return [.. WeatherSourceFactory.CreateWeatherSources()];
  }

}