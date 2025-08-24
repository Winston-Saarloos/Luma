using Luma.API.Configuration;
using Luma.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Luma.API.Services
{
    public class GoogleGeocodingService(
        HttpClient httpClient,
        IOptions<GoogleApiSettings> config,
        ILogger<GoogleGeocodingService> logger,
        IDistributedCache cache
    ) : IGeocodingService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly GoogleApiSettings _config = config.Value;
        private readonly ILogger<GoogleGeocodingService> _logger = logger;
        private readonly IDistributedCache _cache = cache;

        public async Task<(double lat, double lng)> GetCoordinatesAsync(string city)
        {
            var cacheKey = $"geocode:{city.ToLowerInvariant()}";

            var cachedValue = await _cache.GetStringAsync(cacheKey);
            if (cachedValue != null)
            {
                var coords = cachedValue.Split(',');
                return (double.Parse(coords[0]), double.Parse(coords[1]));
            }

            var url = $"{_config.Geocoding.BaseUrl}?address={Uri.EscapeDataString(city)}&key={_config.ApiKey}";
            var httpResponse = await _httpClient.GetAsync(url);
            var json = await httpResponse.Content.ReadAsStringAsync();

            _logger.LogInformation("Full geocoding API response for '{City}': {Json}", city, json);

            var result = System.Text.Json.JsonSerializer.Deserialize<GeoResult>(json);
            var loc = result?.Results?.FirstOrDefault()?.Geometry?.Location;

            if (loc == null)
                throw new Exception($"Geocoding failed for city: {city}");

            var valueToCache = $"{loc.Lat},{loc.Lng}";
            await _cache.SetStringAsync(cacheKey, valueToCache, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            return (loc.Lat, loc.Lng);
        }
    }
}
