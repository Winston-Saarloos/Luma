using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Luma.API.Configuration;
using Luma.API.Models;
using System.Text.Json;

namespace Luma.API.Services
{
    public class GoogleWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleApiSettings _config;
        private readonly IDistributedCache _cache;
        private readonly ILogger<GoogleWeatherService> _logger;

        public GoogleWeatherService(HttpClient httpClient, IDistributedCache cache, IOptions<GoogleApiSettings> config, ILogger<GoogleWeatherService> logger)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _cache = cache;
            _logger = logger;
        }

        public async Task<WeatherDto> GetWeatherAsync(double lat, double lng)
        {
            var cacheKey = $"weather:{lat:F4},{lng:F4}";
            var cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation("Returning cached weather response.");
                return JsonSerializer.Deserialize<WeatherDto>(cachedData)!;
            }

            var url = $"{_config.Weather.BaseUrl}/v1/currentConditions:lookup" +
                $"?key={_config.ApiKey}" +
                $"&location.latitude={lat}" +
                $"&location.longitude={lng}" +
                $"&unitsSystem=METRIC";

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Full weather API response: {json}", json);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Weather API error: {response.StatusCode}");

            var result = JsonSerializer.Deserialize<CurrentWeatherResponse>(json);

            if (result is null)
                throw new Exception("Failed to parse weather data.");

            var weatherResponse = new WeatherDto
            {
                TemperatureC = result.Temperature?.Degrees ?? 0,
                Condition = result.WeatherCondition?.Description?.Text,
                Humidity = result.RelativeHumidity
            };

            var serializedData = JsonSerializer.Serialize(weatherResponse);
            await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return weatherResponse;
        }
    }
}
