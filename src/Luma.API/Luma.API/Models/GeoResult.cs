using System.Text.Json.Serialization;

namespace Luma.API.Models
{
    public class GeoResult
    {
        [JsonPropertyName("results")]
        public List<Result>? Results { get; set; }

        public class Result
        {
            [JsonPropertyName("geometry")]
            public Geometry? Geometry { get; set; }
        }

        public class Geometry
        {
            [JsonPropertyName("location")]
            public Location? Location { get; set; }
        }

        public class Location
        {
            [JsonPropertyName("lat")]
            public double Lat { get; set; }

            [JsonPropertyName("lng")]
            public double Lng { get; set; }
        }
    }
}
