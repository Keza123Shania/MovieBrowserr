using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MovieBrowserr.Data;
using MovieBrowserr.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieBrowserr.Components.Pages
{
    public partial class Watchlist
    {
        private bool isLoading = true;
        private List<WatchlistItem> watchlistMovies = new();
        private string? currentUserId;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId != null)
            {
                watchlistMovies = await DbContext.WatchlistItems
                    .Where(w => w.UserId == currentUserId && !w.IsFavorite)
                    .ToListAsync();
            }
            isLoading = false;
        }

        private async Task RemoveMovie(int itemId)
        {
            var movieToRemove = await DbContext.WatchlistItems.FindAsync(itemId);
            if (movieToRemove != null)
            {
                DbContext.WatchlistItems.Remove(movieToRemove);
                await DbContext.SaveChangesAsync();
                watchlistMovies.Remove(movieToRemove); // Update UI
            }
        }
    }
}
