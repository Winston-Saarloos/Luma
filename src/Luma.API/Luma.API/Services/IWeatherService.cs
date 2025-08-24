using Luma.API.Models;

namespace Luma.API.Services
{
    public interface IWeatherService
    {
        Task<WeatherDto>GetWeatherAsync(double lat, double lng);
    }
}
