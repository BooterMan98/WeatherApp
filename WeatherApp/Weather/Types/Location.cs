namespace WeatherApp.Weather.Types;

/// <summary>
/// A location in which a measurement is made.
/// </summary>
struct Location
{
  public required string Value { get; set; }

  
  public override readonly string ToString() => Value.ToString();
  
  public static implicit operator string(Location location) => location.Value;
  
  public static explicit operator Location(string str) => new() { Value = str };
}