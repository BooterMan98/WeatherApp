using System.Numerics;
using WeatherApp.Weather;

namespace WeatherApp.Interfaces;

interface IWeatherManagerProvider
{
  WeatherManager CreateWeatherManager();

  Task<List<IBot>> CreateBotsAsync();

  IWeatherSource[] GetWeatherSources();


}