# Realtime simulator Weather App

## Project structure
This is what I intend to implement, not necessarily final. Things may have escaped me and I'll try to update this file according to project development.

### DataModels

- record struct WeatherMeasurements // weather Data model
  - Location: Location
  - Temperature: Temperature
  - Humidity: Humidity

- class XMLWeatherReader: IWeatherSource // Read Weather from XML
- class JSONWeatherReader: IWeatherSource // Read Weather from JSON

- class WeatherManager: IWeatherReport

- abstract class WeatherReaderFactory: IWeatherSourceFactory

- class WeatherReflectionReaderFactory: WeatherReaderFactory

- abstract class Bot: IBot


- abstract class BotLoader: IBotLoader

- class JSONBotLoader: BotLoader

- struct Location
- struct Temperature
- struct Humidify



### Interfaces

- IBot: IWeatherListener // Define Bot operations
  - analyze()

- IWeatherReport: // Interface to manage weather reports. Possibly a singleton, a publisher
  - subscribers: IWeatherListener[]
  - subscribe(IWeatherListener listener)
  - unSubscribe(IWeatherListener listener)
  - notifySubscribers()

- IWeatherListener: // An Observer for IWeatherReport Classes
  - update(WeatherMeasurement)

- IWeatherSource: // Interface to read different weather inputs and return a common weather object
  - read() -> WeatherMeasurement
  - read(string str) -> WeatherMeasurement

- IBotLoader:
  - LoadBots() -> IBot[]

- IWeatherSourceFactory:
  - CreateWeatherSources() -> IWeatherSource[]

- IWeatherBotOutput: // Interface between bots and output (Say print to console or to file for example)
  - print()



