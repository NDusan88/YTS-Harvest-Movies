using System.Collections.Generic;
using static YTS.YTSApi;

namespace YTS
{
    interface IYTSApi
    {
        public Root GetMoviePopularDownloads();
        public Root GetMovieBy(int page, int limit, string query_term, string genre, string quality, string sort, string order);
        public Root GetMovieDetails(int id);
        public Root GetMovieSuggestions(int id);
        public Root GetMovies(int page, int limit, string sort, string order);
        public List<Root> GetAllMovies();
    }
}
