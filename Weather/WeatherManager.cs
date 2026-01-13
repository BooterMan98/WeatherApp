using WeatherApp.Interfaces;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather;

class WeatherManager(IWeatherSource[] inputSources) : IWeatherReport {

  private List<IWeatherListener> Subscribers { get; init; } = [];

  private readonly Queue<WeatherMeasurement> pendingMeasurements = [];

  private IWeatherSource[] InputSources { get; init; } = inputSources;



  public void ReceiveRawMeasurement(string data)
  {
    var possibleDataFormats = InputSources.Where( inputSource => inputSource.IsReadable(data));
    foreach (var possibleDataFormat in possibleDataFormats)
    {
      var result = possibleDataFormat.Read(data);
      if (result.IsSuccess)
      {
        ReceiveMeasurement(result.Value);
        break;
      } else
      {
        Console.WriteLine(result.Error);
      }
    }
  }
  public void ReceiveMeasurement(WeatherMeasurement measurement) {
    pendingMeasurements.Enqueue(measurement);
  }

  public void NotifySubscribers()
  {
    while (pendingMeasurements.Count != 0)
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