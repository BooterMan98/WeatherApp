// See https://aka.ms/new-console-template for more information
using WeatherApp.Interfaces;
using WeatherApp.Sources;
using WeatherApp.Weather;

Console.WriteLine("Hello, World!");

// {"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}

/* 
<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>
*/

WeatherManagerProvider provider = new ();
var sources = provider.GetWeatherSources();

var manager = provider.CreateWeatherManager(sources);

foreach (var bot in await provider.CreateBotsAsync("config.json"))
{
  manager.Subscribe(bot);
}

while (true)
{
  var newMeasurementString = Console.ReadLine();
  // var newMeasurement = new JSONWeatherSource().Read(newMeasurementString);
  manager.ReceiveRawMeasurement(newMeasurementString);
  manager.NotifySubscribers();
} 