using Application.DTO;
using Application.Interfaces;
using Application.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ControlEscolarXWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITipoPersonalUseCase _tipoPersonalUse;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITipoPersonalUseCase tipoPersonalUse)
        {
            _logger = logger;
            _tipoPersonalUse = tipoPersonalUse;

        }

        [HttpGet]
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

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await _tipoPersonalUse.GetListTipoPersonalHandler());
        }


    }
}
