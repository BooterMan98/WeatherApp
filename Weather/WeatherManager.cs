using WeatherApp.Interfaces;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather;

class WeatherManager(IWeatherSource[] inputSources) : IWeatherReport {

  private List<IWeatherListener> Subscribers { get; init; } = [];

  private Queue<WeatherMeasurement> pendingMeasurements = [];

  private IWeatherSource[] InputSources { get; init; } = inputSources;
  public void ReceiveMeasurement(WeatherMeasurement measurement) {
    pendingMeasurements.Enqueue(measurement);
  }

  public void NotifySubscribers()
  {
    while (pendingMeasurements.Any())
    {
      var currentMeasurement = pendingMeasurements.Dequeue();
      foreach (var weatherSubscriber in Subscribers)
      {
        weatherSubscriber.Update(currentMeasurement);
      }
    }
  }

  public void Subscribe(IWeatherListener newSubscriber)
  {
    Subscribers.Add(newSubscriber);
  }

  public void UnSubscribe(IWeatherListener subscriber)
  {
    Subscribers.Remove(subscriber);
  }
}