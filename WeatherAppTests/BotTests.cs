using AutoFixture;
using Moq;
using WeatherApp.Weather.Bots;
using WeatherApp.Weather.Models;
using WeatherApp.Weather.Types;
namespace WeatherAppTests;

public class BotTests
{
    /// <summary>
    /// Verifies that the RainBot prints the configured message to the console when the humidity threshold is achieved or exceeded.
    /// </summary>
    /// <param name="threshold">The humidity threshold value to test, ranging from 0 to 100.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(20)]
    [InlineData(40)]
    [InlineData(60)]
    [InlineData(80)]
    [InlineData(100)]
    public void RainBotShouldPrintAMessageToConsoleWhenHumidityThresholdAchieved(decimal threshold)
    {
        // arrange
        var fixture = new Fixture();
        var humidityThreshold = (Humidity)threshold;

        fixture.Customize<BotConfiguration>(c => c
            .Without(c => c.TemperatureThreshold)
            .With(c => c.HumidityThreshold, humidityThreshold)
        );

        fixture.Customize<WeatherMeasurement>(c => c
        .With(w => w.Humidity,
            (Humidity humidity) => (humidity < humidityThreshold) ? humidityThreshold : humidity)
        );

        var measurement = fixture.Create<WeatherMeasurement>();
        var botConfiguration = fixture.Create<BotConfiguration>();

        var message = botConfiguration.Message;

        using (StringWriter sw = new ())
        {

            var output = Console.Out;
            Console.SetOut(sw);

            // act
            var bot = new RainBot(botConfiguration);
            bot.Update(measurement);

            Console.SetOut(output);

            Assert.Equal(message, sw.ToString().Trim());
            
        }
        // dispose
    }
}
