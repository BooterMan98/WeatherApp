# Realtime simulator Weather App

## Project structure
This is what I intend to implement, not necessarily final. Things may have escaped me and I'll try to update this file according to project development.

### DataModels

- record struct Weather // weather Data model
  - Location: Location
  - Temperature: Temperature
  - Humidity: Humidity

- class XMLWeatherReader: IWeatherSource // Read Weather from XML
- class JSONWeatherReader: IWeatherSource // Read Weather from JSON

- class WeatherBot: IWeatherBot

- abstract class WeatherManager: IWeatherReport
  - botLoader: BotLoader
  

- class StandardWeatherManager: WeatherManager

- class ConsoleController: IWeatherBotOutput

- abstract class BotLoader // bot factory

- class JSONBotLoader: BotLoader

- struct Location
- struct Temperature
- struct Humidify



### Interfaces

- IWeatherBot: IWeatherListener // Define Bot operations
  - analyze()

- IWeatherReport: // Interface to manage weather reports. Possibly a singleton, a publisher
  - subscribers: IWeatherListener[]
  - subscribe(IWeatherListener listener)
  - unSubscribe(IWeatherListener listener)
  - notifySubscribers()

- IWeatherListener: // An Observer for IWeatherReport Classes
  - update()

- IWeatherSource: // Interface to read different weather inputs and return a common weather object
  - read() -> Weather
  - read(string str) -> Weather


- IWeatherBotOutput: // Interface between bots and output (Say print to console or to file for example)
  - print()





###