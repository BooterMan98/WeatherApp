namespace WeatherApp.Interfaces;

/// <summary>
/// Publisher of observer pattern for weather measurements 
/// </summary>
interface IWeatherReport
{
  void Subscribe(IWeatherListener newSubscriber);
  void UnSubscribe(IWeatherListener subscriber);

  void NotifySubscribers();


}