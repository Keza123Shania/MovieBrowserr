using Microsoft.AspNetCore.Components;
using MovieBrowserr.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using MovieBrowserr.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieBrowserr.Components.Pages
{
    public partial class MovieDetails
    {
        [Parameter]
        public int MovieId { get; set; }

        private MovieDetail? movie;
        private List<Movie> similarMovies = new();
        private string apiKey = "01e36c2ef3033c34ac637091981746c3";
        private string? currentUserId;

        private bool isOnWatchlist = false;
        private bool isFavorite = false;

        protected override async Task OnParametersSetAsync()
        {
            movie = null;
            similarMovies = new();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            currentUserId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await LoadMovieData();
        }

        private async Task LoadMovieData()
        {
            var movieDetailTask = Http.GetFromJsonAsync<MovieDetail>($"https://api.themoviedb.org/3/movie/{MovieId}?api_key={apiKey}");
            var similarMoviesTask = Http.GetFromJsonAsync<MovieSearchResult>($"https://api.themoviedb.org/3/movie/{MovieId}/similar?api_key={apiKey}");

            await Task.WhenAll(movieDetailTask, similarMoviesTask);

            movie = movieDetailTask.Result;
            similarMovies = similarMoviesTask.Result?.Results ?? new List<Movie>();

            if (currentUserId != null)
            {
                isOnWatchlist = await DbContext.WatchlistItems.AnyAsync(i => i.UserId == currentUserId && i.MovieId == MovieId && !i.IsFavorite);
                isFavorite = await DbContext.WatchlistItems.AnyAsync(i => i.UserId == currentUserId && i.MovieId == MovieId && i.IsFavorite);
            }

            StateHasChanged();
        }

        private async Task ToggleWatchlist()
        {
            if (currentUserId is null || movie is null) return;

            var existingItem = await DbContext.WatchlistItems.FirstOrDefaultAsync(i => i.UserId == currentUserId && i.MovieId == MovieId && !i.IsFavorite);

            if (existingItem != null)
            {
                DbContext.WatchlistItems.Remove(existingItem);
                isOnWatchlist = false;
            }
            else
            {
                var newItem = new WatchlistItem
                {
                    UserId = currentUserId,
                    MovieId = movie.Id,
                    Title = movie.Title,
                    PosterPath = $"https://image.tmdb.org/t/p/w500/{movie.PosterPath}",
                    IsFavorite = false
                };
                DbContext.WatchlistItems.Add(newItem);
                isOnWatchlist = true;
            }
            await DbContext.SaveChangesAsync();
        }

        private async Task ToggleFavorite()
        {
            if (currentUserId is null || movie is null) return;

            var existingItem = await DbContext.WatchlistItems.FirstOrDefaultAsync(i => i.UserId == currentUserId && i.MovieId == MovieId && i.IsFavorite);

            if (existingItem != null)
            {
                DbContext.WatchlistItems.Remove(existingItem);
                isFavorite = false;
            }
            else
            {
                var newItem = new WatchlistItem
                {
                    UserId = currentUserId,
                    MovieId = movie.Id,
                    Title = movie.Title,
                    PosterPath = $"https://image.tmdb.org/t/p/w500/{movie.PosterPath}",
                    IsFavorite = true
                };
                DbContext.WatchlistItems.Add(newItem);
                isFavorite = true;
            }
            await DbContext.SaveChangesAsync();
        }
    }
}

