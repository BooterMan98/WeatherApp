using WeatherApp.Weather.Models;

namespace WeatherApp.Interfaces;

 
interface IWeatherSource
{
  WeatherMeasurement Read();
  WeatherMeasurement Read(string url);
}