namespace WeatherApp.Interfaces;

/// <summary>
/// Publisher of observer pattern for weather measurements 
/// </summary>
interface IWeatherReport
{
  List<IWeatherListener> Subscribers {get; set;}

  void Subscribe(IWeatherListener newSubscriber);
  void UnSubscribe(IWeatherListener subscriber);

  void NotifySubscribers();


}