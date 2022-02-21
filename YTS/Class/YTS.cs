using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace YTS
{
    public class YTSApi : IYTSApi
    {
        Root root = new Root();

        ///<summary>
        ///Get All movies from YTS
        ///</summary>
        public List<Root> GetAllMovies()
        {
            List<Root> list = new List<Root>();
            int movie_count = 0;
            int counter = 1;
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/list_movies.json?page=1&limit=50&sort_by=year&order_by=desc");
                root = JsonSerializer.Deserialize<Root>(jsonString);
                movie_count = root.Data.MovieCount / 50;
                while (counter <= movie_count)
                {
                    Thread.Sleep(1000);
                    jsonString = wc.DownloadString($"https://yts.mx/api/v2/list_movies.json?page={counter}&limit=50&sort_by=year&order_by=desc");
                    root = JsonSerializer.Deserialize<Root>(jsonString);
                    list.Add(root);
                    counter++;
                    System.Console.WriteLine(counter);
                }
            }            
            return list;
        }

        ///<summary>
        ///Used for movie search, matching on: Movie Title/IMDb Code, Actor Name/IMDb Code, Director Name/IMDb Code
        ///</summary>
        public Root GetMovieBy(string query_term)
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/list_movies.json?query_term={query_term}");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root;
        }
        public int GetPages()
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/list_movies.json");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root.Data.MovieCount / 50;
        }

        ///<summary>
        ///Get Movie details by movie <paramref name="id"/>
        ///</summary>
        public Root GetMovieDetails(int id)
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/movie_details.json?movie_id={id}&with_images=true&with_cast=true");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root;
        }

        public Root GetMoviePopularDownloads()
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/list_movies.json?page=1&limit=50&sort_by=year&order_by=desc");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root;
        }

        ///<summary>
        ///Get Movies by 
        ///<paramref name="page"/>
        ///<paramref name="limit"/>
        ///<paramref name="sort"/>
        ///<paramref name="order"/>
        ///properties
        ///</summary>
        public Root GetMovies(int page, int limit, string sort, string order)
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString =wc.DownloadString($"https://yts.mx/api/v2/list_movies.json?page={page}&limit={limit}&sort_by={sort}&order_by={order}");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root;
        }

        ///<summary>
        ///Get Movie Suggestions by movie <paramref name="id"/>
        ///</summary>
        public Root GetMovieSuggestions(int id)
        {
            using (WebClient wc = new WebClient())
            {
                var jsonString = wc.DownloadString($"https://yts.mx/api/v2/movie_suggestions.json?movie_id={id}");
                root = JsonSerializer.Deserialize<Root>(jsonString);
            }

            return root;
        }

        public class Cast
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("character_name")]
            public string CharacterName { get; set; }

            [JsonPropertyName("url_small_image")]
            public string UrlSmallImage { get; set; }

            [JsonPropertyName("imdb_code")]
            public string ImdbCode { get; set; }
        }

        public class Torrent
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("hash")]
            public string Hash { get; set; }

            [JsonPropertyName("quality")]
            public string Quality { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("seeds")]
            public int Seeds { get; set; }

            [JsonPropertyName("peers")]
            public int Peers { get; set; }

            [JsonPropertyName("size")]
            public string Size { get; set; }

            [JsonPropertyName("size_bytes")]
            public long SizeBytes { get; set; }

            [JsonPropertyName("date_uploaded")]
            public string DateUploaded { get; set; }

            [JsonPropertyName("date_uploaded_unix")]
            public int DateUploadedUnix { get; set; }
        }

        public class Movie
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("imdb_code")]
            public string ImdbCode { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("title_english")]
            public string TitleEnglish { get; set; }

            [JsonPropertyName("title_long")]
            public string TitleLong { get; set; }

            [JsonPropertyName("slug")]
            public string Slug { get; set; }

            [JsonPropertyName("year")]
            public int Year { get; set; }

            [JsonPropertyName("rating")]
            public double Rating { get; set; }

            [JsonPropertyName("runtime")]
            public int Runtime { get; set; }

            [JsonPropertyName("genres")]
            public List<string> Genres { get; set; }

            [JsonPropertyName("download_count")]
            public int DownloadCount { get; set; }

            [JsonPropertyName("like_count")]
            public int LikeCount { get; set; }

            [JsonPropertyName("description_intro")]
            public string DescriptionIntro { get; set; }

            [JsonPropertyName("description_full")]
            public string DescriptionFull { get; set; }

            [JsonPropertyName("yt_trailer_code")]
            public string YtTrailerCode { get; set; }

            [JsonPropertyName("language")]
            public string Language { get; set; }

            [JsonPropertyName("mpa_rating")]
            public string MpaRating { get; set; }

            [JsonPropertyName("background_image")]
            public string BackgroundImage { get; set; }

            [JsonPropertyName("background_image_original")]
            public string BackgroundImageOriginal { get; set; }

            [JsonPropertyName("small_cover_image")]
            public string SmallCoverImage { get; set; }

            [JsonPropertyName("medium_cover_image")]
            public string MediumCoverImage { get; set; }

            [JsonPropertyName("large_cover_image")]
            public string LargeCoverImage { get; set; }

            [JsonPropertyName("cast")]
            public List<Cast> Cast { get; set; }

            [JsonPropertyName("torrents")]
            public List<Torrent> Torrents { get; set; }

            [JsonPropertyName("date_uploaded")]
            public string DateUploaded { get; set; }

            [JsonPropertyName("date_uploaded_unix")]
            public int DateUploadedUnix { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("movie")]
            public Movie Movie { get; set; }
            [JsonPropertyName("movies")]
            public List<Movie> Movies { get; set; }
            [JsonPropertyName("movie_count")]
            public int MovieCount { get; set; }

            [JsonPropertyName("limit")]
            public int Limit { get; set; }

            [JsonPropertyName("page_number")]
            public int PageNumber { get; set; }
        }

        public class Meta
        {
            [JsonPropertyName("server_time")]
            public int ServerTime { get; set; }

            [JsonPropertyName("server_timezone")]
            public string ServerTimezone { get; set; }

            [JsonPropertyName("api_version")]
            public int ApiVersion { get; set; }

            [JsonPropertyName("execution_time")]
            public string ExecutionTime { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("status_message")]
            public string StatusMessage { get; set; }

            [JsonPropertyName("data")]
            public Data Data { get; set; }

            [JsonPropertyName("@meta")]
            public Meta Meta { get; set; }
        }
    }
}
