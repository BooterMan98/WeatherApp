using WeatherApp.Weather.Models;

namespace WeatherApp.Interfaces;

interface IBot: IWeatherListener
{
  void Analyze(WeatherMeasurement measurement);
}