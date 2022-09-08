using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherDelayedController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Broken", "Overcast", "Few", "Scattered"
    };

    private readonly ILogger<WeatherDelayedController> _logger;

    public WeatherDelayedController(ILogger<WeatherDelayedController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherDelayed")]
    public IEnumerable<WeatherForecast> Get()
    {

        Random sleepTime = new Random();
        var seconds = sleepTime.Next(10);
        if (seconds == 9) {
            seconds *= 4;
            _logger.LogWarning("Uh oh, taking a randomly long amount of time");
        } else if (seconds < 8) {
            seconds = 0;
        } 

        System.Threading.Thread.Sleep(seconds*1000);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

}
