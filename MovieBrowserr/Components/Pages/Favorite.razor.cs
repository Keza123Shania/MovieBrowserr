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
    public partial class Favorites
    {
        private bool isLoading = true;
        private List<WatchlistItem> favoriteMovies = new();
        private string? currentUserId;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId != null)
            {
                favoriteMovies = await DbContext.WatchlistItems
                    .Where(w => w.UserId == currentUserId && w.IsFavorite)
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
                favoriteMovies.Remove(movieToRemove); // Update UI
            }
        }
    }
}

