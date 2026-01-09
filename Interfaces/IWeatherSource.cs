using WeatherApp.Weather.Models;

namespace WeatherApp.Interfaces;

 
interface IWeatherSource
{
  WeatherMeasurement Read();
}

interface IWeatherSourceAsync {
  Task<WeatherMeasurement> ReadAsync();

}