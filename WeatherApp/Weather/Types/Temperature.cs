namespace WeatherApp.Weather.Types;
/// <summary>
/// Temperature measurement in Celsius.
/// </summary>
struct Temperature
{
  public required decimal Value { get; set; }

  public override readonly string ToString() => $"{Value}°C";

  public static implicit operator decimal(Temperature temperature) => temperature.Value;

  public static implicit operator Temperature(decimal dec) => new() { Value = dec };
}