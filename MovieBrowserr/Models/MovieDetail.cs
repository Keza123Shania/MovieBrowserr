using System.Text.Json.Serialization;

namespace MovieBrowser.Models
{
    public class MovieDetail
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = "";

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = "";

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = "";

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new();

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; } 

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
    }
}
    