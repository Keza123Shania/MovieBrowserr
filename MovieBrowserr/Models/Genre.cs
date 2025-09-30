using System.Text.Json.Serialization;

namespace MovieBrowser.Models
{
    public class Genre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}