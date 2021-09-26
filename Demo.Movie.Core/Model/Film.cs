
using Newtonsoft.Json;

namespace Demo.Movie.Core.Model
{
    public class Film
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public string title { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string overview { get; set; }
        public int[] genre_ids { get; set; }
        public decimal popularity { get; set; }
        public int vote_count { get; set; }
        public decimal vote_average { get; set; }

        [JsonIgnore]
        public string poster_url { get; set; }

    }
}
