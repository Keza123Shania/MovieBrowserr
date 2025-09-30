using System.ComponentModel.DataAnnotations;

namespace MovieBrowserr.Models
{
    public class WatchlistItem
    {
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        public int MovieId { get; set; }

        public string? Title { get; set; }

        public string? PosterPath { get; set; }

        public bool IsFavorite { get; set; } // True for favorites, false for watchlist
    }
}

