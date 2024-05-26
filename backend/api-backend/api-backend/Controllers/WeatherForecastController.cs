using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly HttpClient _httpClient;

        public WeatherForecastController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("weather-forecast", Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("medicao-tempo", Name = "GetContentApi")]
        public async Task<IEnumerable<JsonDocument>> GetContentApi()
        {
            string url = "https://api.hgbrasil.com/weather";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var jsonDocument = await JsonDocument.ParseAsync(responseStream);

            return new List<JsonDocument> { jsonDocument };
        }
    }

    
}
