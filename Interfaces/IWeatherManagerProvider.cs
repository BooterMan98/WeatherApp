using System.Numerics;
using WeatherApp.Weather;

namespace WeatherApp.Interfaces;

interface IWeatherManagerProvider
{
  WeatherManager CreateWeatherManager(IWeatherSource[] possibleWeatherSources);

  Task<List<IBot>> CreateBotsAsync(string configLocation);

  IWeatherSource[] GetWeatherSources();


}