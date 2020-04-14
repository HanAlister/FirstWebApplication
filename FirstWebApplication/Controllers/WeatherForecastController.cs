using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FirstWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const string URL = "https://api.openweathermap.org/data/2.5/forecast";
        private string urlParameters = "?appid=5e26fb10acb3dc8c3ace4a6f68913ee9&lang=pl";


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Object>> GetAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                HttpResponseMessage response = client.GetAsync(urlParameters + $"&id=3081368&units=metric").Result;

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<WeatherForecast>(responseBody);

                return jsonObject.List.Select(fd => new { 
                    date = fd.DtTxt.ToString("dd/MM/yyyy hh:mm"),
                    fd.Main.Temp, 
                    fd.Main.Pressure, 
                    fd.Main.Humidity, 
                    Clouds = fd.Clouds?.All, 
                    Rain = fd.Rain?.The3H.ToString() ?? "Brak", 
                    Snow = fd.Snow?.The3H.ToString() ?? "Brak", 
                    fd.Weather.FirstOrDefault().Description });
            }
        }
    }
}
