using System.ComponentModel.DataAnnotations;

namespace MovieBrowser.Models
{
    public class WatchlistItem
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = "";
        public string? PosterPath { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
