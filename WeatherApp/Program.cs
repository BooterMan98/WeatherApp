using WeatherApp.Weather;

// {"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}

/* 
<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>
*/

WeatherManagerProvider provider = new ();
var manager = provider.CreateWeatherManager();



while (true)
{
  var newMeasurementString = Console.ReadLine();
  // var newMeasurement = new JSONWeatherSource().Read(newMeasurementString);
  manager.ReceiveRawMeasurement(newMeasurementString);
  manager.NotifySubscribers();
} 