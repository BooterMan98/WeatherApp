using WeatherApp.Interfaces;
using WeatherApp.Weather.Bots;

namespace WeatherApp.Weather.Factories;

abstract class BotLoader : IBotLoader
{

  abstract protected List<Bot> LoadBotsInternal();

  // Done to cast Bot as IBot
  public List<IBot> LoadBots() => [.. LoadBotsInternal()];
}