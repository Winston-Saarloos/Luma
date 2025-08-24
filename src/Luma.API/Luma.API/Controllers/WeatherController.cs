using Microsoft.AspNetCore.Mvc;
using Luma.API.Services;

namespace Luma.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IGeocodingService _geo;
        private readonly IWeatherService _weather;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(
            IGeocodingService geo,
            IWeatherService weather,
            ILogger<WeatherController> logger)
        {
            _geo = geo;
            _weather = weather;
            _logger = logger;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                _logger.LogInformation("Request received for weather in city: {City}", city);

                var (lat, lng) = await _geo.GetCoordinatesAsync(city);
                var weather = await _weather.GetWeatherAsync(lat, lng);

                return Ok(weather); // this is a WeatherDto
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve weather for city: {City}", city);
                return StatusCode(500, new { error = "Unable to retrieve weather data." });
            }
        }
    }
}
