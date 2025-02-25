using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI_1.Controllers
{
    [ApiController]//表示这是一个WebApi的Controller
    [Route("[controller]")]//表示用户请求时用方括号中的名字来请求（此处是WeatherForecast，去掉控制器类名后的Controller控制器名称）
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]//表示当向[Route("[controller]")]发出get请求时，由此方法处理
        //[HttpGet(Name = "GetWeatherForecast")] 属性为方法指定了路由名称 "GetWeatherForecast" 后，可以在控制器的其他方法中使用 Url.Action 方法根据该路由名称生成 URL。
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
    }
}
