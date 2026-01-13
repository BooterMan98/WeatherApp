using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Weather.Bots;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather.Factories;

abstract class BotLoader : IBotLoader
{

  abstract protected List<Bot> LoadBotsInternal();
  
  abstract public Result<Bot> LoadBot(BotConfiguration configuration);

  // Done to cast Bot as IBot
  public List<IBot> LoadBots() => [.. LoadBotsInternal()];
}