using System.Text.Json.Serialization;

namespace MovieBrowserr.Models
{
    public class Genre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}