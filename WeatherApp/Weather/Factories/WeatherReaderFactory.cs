using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather.Factories;

abstract class WeatherReaderFactory : IWeatherSourceFactory
{

  abstract public List<IWeatherSource> CreateWeatherSources();

}