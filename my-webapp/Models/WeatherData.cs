namespace my_webapp.Models;

public class WeatherDataModel
{
    public DateTime date { get; set; }
    public int temperatureC {get; set; }
    public int temperature { get; set; }
    public string? summary { get; set; }
}