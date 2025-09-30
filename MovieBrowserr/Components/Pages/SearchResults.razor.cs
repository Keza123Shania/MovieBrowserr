using Microsoft.AspNetCore.Components;
using MovieBrowserr.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieBrowserr.Components.Pages
{
    public partial class SearchResults
    {
        [Parameter]
        public string Query { get; set; } = "";

        private List<Movie>? movies;
        private int currentPage = 1;
        private int totalPages = 1;
        private bool isLoading = true;
        private string apiKey = "01e36c2ef3033c34ac637091981746c3";

        protected override async Task OnParametersSetAsync()
        {
            await SearchMovies(1);
        }

        private async Task SearchMovies(int pageNumber)
        {
            isLoading = true;
            StateHasChanged();

            currentPage = pageNumber;
            var url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(Query)}&page={currentPage}";
            var searchResult = await Http.GetFromJsonAsync<MovieSearchResult>(url);

            if (searchResult != null)
            {
                movies = searchResult.Results;
                totalPages = searchResult.TotalPages;
            }

            isLoading = false;
        }
    }
}
