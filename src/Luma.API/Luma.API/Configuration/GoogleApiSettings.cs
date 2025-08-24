namespace Luma.API.Configuration
{
    public class GoogleApiSettings
    {
        public string ApiKey { get; set; } = string.Empty;

        public WeatherSettings Weather { get; set; } = new();
        public GeocodingSettings Geocoding { get; set; } = new();

        public class WeatherSettings
        {
            public string BaseUrl { get; set; } = string.Empty;
        }

        public class GeocodingSettings
        {
            public string BaseUrl { get; set; } = string.Empty;
        }
    }
}

