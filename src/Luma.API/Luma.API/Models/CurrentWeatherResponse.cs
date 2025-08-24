using System.Text.Json.Serialization;

namespace Luma.API.Models
{
    public class CurrentWeatherResponse
    {
        [JsonPropertyName("currentTime")]
        public string CurrentTime { get; set; }

        [JsonPropertyName("timeZone")]
        public TimeZoneInfo? TimeZone { get; set; }

        [JsonPropertyName("isDaytime")]
        public bool IsDaytime { get; set; }

        [JsonPropertyName("weatherCondition")]
        public WeatherCondition? WeatherCondition { get; set; }

        [JsonPropertyName("temperature")]
        public Temperature? Temperature { get; set; }

        [JsonPropertyName("feelsLikeTemperature")]
        public Temperature? FeelsLikeTemperature { get; set; }

        [JsonPropertyName("dewPoint")]
        public Temperature? DewPoint { get; set; }

        [JsonPropertyName("heatIndex")]
        public Temperature? HeatIndex { get; set; }

        [JsonPropertyName("windChill")]
        public Temperature? WindChill { get; set; }

        [JsonPropertyName("relativeHumidity")]
        public int RelativeHumidity { get; set; }

        [JsonPropertyName("uvIndex")]
        public int UvIndex { get; set; }

        [JsonPropertyName("precipitation")]
        public Precipitation? Precipitation { get; set; }

        [JsonPropertyName("thunderstormProbability")]
        public int ThunderstormProbability { get; set; }

        [JsonPropertyName("airPressure")]
        public AirPressure? AirPressure { get; set; }

        [JsonPropertyName("wind")]
        public Wind? Wind { get; set; }

        [JsonPropertyName("visibility")]
        public Visibility? Visibility { get; set; }

        [JsonPropertyName("cloudCover")]
        public int CloudCover { get; set; }

        [JsonPropertyName("currentConditionsHistory")]
        public CurrentConditionsHistory? CurrentConditionsHistory { get; set; }
    }

    public class TimeZoneInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class WeatherCondition
    {
        [JsonPropertyName("iconBaseUri")]
        public string? IconBaseUri { get; set; }

        [JsonPropertyName("description")]
        public Description? Description { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Description
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("languageCode")]
        public string? LanguageCode { get; set; }
    }

    public class Temperature
    {
        [JsonPropertyName("degrees")]
        public double Degrees { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }
    }

    public class Precipitation
    {
        [JsonPropertyName("probability")]
        public Probability? Probability { get; set; }

        [JsonPropertyName("snowQpf")]
        public Quantity? SnowQpf { get; set; }

        [JsonPropertyName("qpf")]
        public Quantity? Qpf { get; set; }
    }

    public class Probability
    {
        [JsonPropertyName("percent")]
        public int Percent { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Quantity
    {
        [JsonPropertyName("quantity")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }
    }

    public class AirPressure
    {
        [JsonPropertyName("meanSeaLevelMillibars")]
        public double MeanSeaLevelMillibars { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("direction")]
        public WindDirection? Direction { get; set; }

        [JsonPropertyName("speed")]
        public WindSpeed? Speed { get; set; }

        [JsonPropertyName("gust")]
        public WindSpeed? Gust { get; set; }
    }

    public class WindDirection
    {
        [JsonPropertyName("degrees")]
        public int Degrees { get; set; }

        [JsonPropertyName("cardinal")]
        public string? Cardinal { get; set; }
    }

    public class WindSpeed
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }
    }

    public class Visibility
    {
        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }
    }

    public class CurrentConditionsHistory
    {
        [JsonPropertyName("temperatureChange")]
        public Temperature? TemperatureChange { get; set; }

        [JsonPropertyName("maxTemperature")]
        public Temperature? MaxTemperature { get; set; }

        [JsonPropertyName("minTemperature")]
        public Temperature? MinTemperature { get; set; }

        [JsonPropertyName("snowQpf")]
        public Quantity? SnowQpf { get; set; }

        [JsonPropertyName("qpf")]
        public Quantity? Qpf { get; set; }
    }
}
