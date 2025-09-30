using System.Text.Json.Serialization;

namespace MovieBrowser.Models
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public required string? Title { get; set; }

        [JsonPropertyName("poster_path")]
        public required string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public required string ReleaseDate { get; set; }
    }
}
