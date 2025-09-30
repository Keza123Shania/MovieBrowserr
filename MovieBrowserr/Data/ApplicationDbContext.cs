using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBrowserr.Models; // Corrected namespace

namespace MovieBrowserr.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        // THIS LINE IS THE CRITICAL FIX for the database error.
        // It tells Entity Framework to include the WatchlistItem table in the database.
        public DbSet<WatchlistItem> WatchlistItems { get; set; }
    }
}
