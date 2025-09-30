using System.Text.Json.Serialization;
using MovieBrowser.Models; // Make sure this using is here

namespace MovieBrowser.Models
{
    public class MovieSearchResult
    {
        [JsonPropertyName("results")]
        public List<Movie> Results { get; set; } = new();

        // NEW PROPERTIES
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}