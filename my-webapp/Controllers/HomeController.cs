using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using my_webapp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace my_webapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    string WeatherForecastURL = "https://ase-devops-app-fsp-9122-2-dt.azurewebsites.net/";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Broken()
    {
        var divisor = 0;
        var myNumber = 10/divisor;
        return View();
    }

    public async Task<ActionResult> Weather()
    {
        List<WeatherDataModel> WeatherInfo = new List<WeatherDataModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(WeatherForecastURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Resp = await client.GetAsync("WeatherForecast");

            if (Resp.IsSuccessStatusCode) {
                var WeatherResult = Resp.Content.ReadAsStringAsync().Result;                
                WeatherInfo = JsonConvert.DeserializeObject<List<WeatherDataModel>>(WeatherResult);
            }
        }

        return View(WeatherInfo);
    }

        public async Task<ActionResult> WeatherDelayed()
    {
        List<WeatherDataModel> WeatherInfo = new List<WeatherDataModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(WeatherForecastURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Resp = await client.GetAsync("WeatherDelayed");

            if (Resp.IsSuccessStatusCode) {
                var WeatherResult = Resp.Content.ReadAsStringAsync().Result;                
                WeatherInfo = JsonConvert.DeserializeObject<List<WeatherDataModel>>(WeatherResult);
            }
        }

        return View(WeatherInfo);
    }

    public async Task<ActionResult> WeatherBroken()
    {
        List<WeatherDataModel> WeatherInfo = new List<WeatherDataModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(WeatherForecastURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Resp = await client.GetAsync("WeatherForecastDoesNotExist");

            if (Resp.IsSuccessStatusCode) {
                var WeatherResult = Resp.Content.ReadAsStringAsync().Result;                
                WeatherInfo = JsonConvert.DeserializeObject<List<WeatherDataModel>>(WeatherResult);
            }
        }

        return View(WeatherInfo);
    }

    public async Task<ActionResult> WeatherBrokenHandled()
    {
        List<WeatherDataModel> WeatherInfo = new List<WeatherDataModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(WeatherForecastURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Resp = await client.GetAsync("WeatherForecastDoesNotExist");

            if (Resp.IsSuccessStatusCode) {
                var WeatherResult = Resp.Content.ReadAsStringAsync().Result;                
                WeatherInfo = JsonConvert.DeserializeObject<List<WeatherDataModel>>(WeatherResult);
            } else {
                _logger.LogWarning("Calls to weather service not returning success, there could be a problem");
            }
        }

        return View(WeatherInfo);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
