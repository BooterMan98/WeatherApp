namespace WeatherApp.Interfaces;

interface IWeatherSourceFactory
{
  public List<IWeatherSource> CreateWeatherSources();
}