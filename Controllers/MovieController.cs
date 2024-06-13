using MovieAppTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppTest.Controllers
{
    public class MovieController
    {
        public static string url = "https://api.themoviedb.org/";
        private async Task<string> RESTGet(string endpoint)
        {
            string token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhMzcxNzQ2N2M5YzA1MDU0ZDM4Mjc2Nzc1YWRlNDE2ZCIsInN1YiI6IjY2Njk2ZjI2NThjYjkxYzY3ZGE3MjJlNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.g8eFSgI5WR0H1aGcnLUaaOuLGJSzYBkwgrICO6fXXPI";
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("accept", "application/json");
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = await http.GetAsync(endpoint).ConfigureAwait(true);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                return content;
            }
            return string.Empty;
        }

        public async Task<MovieList> List(int page = 1, int? genre = null)
        {
            string genreString = "";
            if (genre != null)
            {
                genreString = $"&with_genres={genre}";
            }
            var result = await RESTGet($"{url}3/discover/movie?page={page}{genreString}").ConfigureAwait(true);
            if (!string.IsNullOrEmpty(result))
            {
                var tmp = JsonConvert.DeserializeObject<MovieList>(result);
                return tmp;
            }
            return null;
        }

        public async Task<MovieDetail> Detail(int id)
        {
            if (id != 0)
            {
                var result = await RESTGet($"{url}3/movie/{id}").ConfigureAwait(true);
                if (!string.IsNullOrEmpty(result))
                {
                    var tmp = JsonConvert.DeserializeObject<MovieDetail>(result);
                    return tmp;
                }
            }
            return null;
        }

        public async Task<List<MovieGenreList>> Genre()
        {
            var result = await RESTGet($"{url}3/genre/movie/list").ConfigureAwait(true);
            if (!string.IsNullOrEmpty(result))
            {
                var tmp = JsonConvert.DeserializeObject<MovieGenre>(result);
                return tmp.genres;
            }
            return null;
        }

        public async Task<UserReview> ReviewList(int id, int page = 1)
        {
            var result = await RESTGet($"{url}3/movie/{id}/reviews?page={page}").ConfigureAwait(true);
            if (!string.IsNullOrEmpty(result))
            {
                var tmp = JsonConvert.DeserializeObject<UserReview>(result);
                return tmp;
            }
            return null;
        }
    }
}
