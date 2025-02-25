using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI_1.Controllers
{
    [ApiController]//��ʾ����һ��WebApi��Controller
    [Route("[controller]")]//��ʾ�û�����ʱ�÷������е����������󣨴˴���WeatherForecast��ȥ���������������Controller���������ƣ�
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

        [HttpGet(Name = "GetWeatherForecast")]//��ʾ����[Route("[controller]")]����get����ʱ���ɴ˷�������
        //[HttpGet(Name = "GetWeatherForecast")] ����Ϊ����ָ����·������ "GetWeatherForecast" �󣬿����ڿ�����������������ʹ�� Url.Action �������ݸ�·���������� URL��
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
