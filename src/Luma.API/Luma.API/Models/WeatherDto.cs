namespace Luma.API.Models
{
    public class WeatherDto
    {
        public double TemperatureC { get; set; }
        public string? Condition { get; set; }
        public double Humidity { get; set; }
    }
}
