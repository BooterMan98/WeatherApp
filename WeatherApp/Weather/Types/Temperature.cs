namespace WeatherApp.Weather.Types;
/// <summary>
/// Temperature measurement in Celsius.
/// </summary>
readonly struct Temperature(decimal value)
{
  public required decimal Value { get; init; } = value;

  public override readonly string ToString() => $"{Value}°C";

  public static implicit operator decimal(Temperature temperature) => temperature.Value;

  public static implicit operator Temperature(decimal dec) => new() { Value = dec };

  public static implicit operator Temperature(int number) => new() { Value = number };
}