using System.Reflection;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;

namespace WeatherApp.Weather.Factories;

class WeatherReflectionReaderFactory : WeatherReaderFactory
{
  private Result<IWeatherSource> CreateWeatherReader(Type weatherReaderType)
  {
      var constructor = weatherReaderType.GetConstructor([]);
      if (constructor is null) return Result.Fail<IWeatherSource>("No constructor found");

      var source = (IWeatherSource)constructor.Invoke([]) ?? throw new NullReferenceException("Couldn't instantiate the weatherSource");
      return Result.Ok(source);
  }

  public override List<IWeatherSource> CreateWeatherSources()
  {
    var sourceTypes = Assembly.GetExecutingAssembly().GetTypes()
      .Where(type => type.IsAssignableTo(typeof(IWeatherSource)));
    var sourceList = new List<IWeatherSource>();

    foreach (var type in sourceTypes)
    {
      var reader = CreateWeatherReader(type);
      if (reader.IsSuccess)
      {
        sourceList.Add(reader.Value);
      }
    }
    return sourceList;
  }
}