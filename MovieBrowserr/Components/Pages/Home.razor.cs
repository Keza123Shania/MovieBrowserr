using Microsoft.AspNetCore.Components;
using MovieBrowserr.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieBrowserr.Components.Pages
{
    public partial class Home
    {
        private List<Movie>? nowPlayingMovies;
        private List<Movie>? topRatedMovies;
        private bool isLoading = true;
        private string apiKey = "01e36c2ef3033c34ac637091981746c3";

        protected override async Task OnInitializedAsync()
        {
            var nowPlayingTask = Http.GetFromJsonAsync<MovieSearchResult>($"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}");
            var topRatedTask = Http.GetFromJsonAsync<MovieSearchResult>($"https://api.themoviedb.org/3/movie/top_rated?api_key={apiKey}");

            await Task.WhenAll(nowPlayingTask, topRatedTask);

            nowPlayingMovies = nowPlayingTask.Result?.Results;
            topRatedMovies = topRatedTask.Result?.Results;

            isLoading = false;
        }
    }
}

