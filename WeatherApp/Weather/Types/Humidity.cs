using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Weather.Types;

/// <summary>
/// Relative Humidity measurement. Value must be between 0.0 and 100.0 inclusive.
/// </summary>
/// <exception cref="ArgumentOutOfRangeException">
/// The value must be between 0.0 and 100.0 inclusive
/// </exception>
readonly struct Humidity(decimal value)
{

  [Range(0, 100)]
  public required decimal Value { 
    get;
    init
    {
      field = (value >= 0 && value <= 100)
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0.0 and 100.0 inclusive");
    } 
    } = value;

  public override readonly string ToString() => $"{Value}%";

  public static implicit operator decimal(Humidity humidity) => humidity.Value;

/// <exception cref="ArgumentOutOfRangeException">
/// The value must be between 0.0 and 100.0 inclusive
/// </exception>
  public static explicit operator Humidity(int newInt) =>  new() { Value = newInt };
/// <exception cref="ArgumentOutOfRangeException">
/// The value must be between 0.0 and 100.0 inclusive
/// </exception>
  public static explicit operator Humidity(decimal dec) => new() { Value = dec };
}