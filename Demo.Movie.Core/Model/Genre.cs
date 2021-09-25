
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.Movie.Core.Model
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public List<Film> associated_films { get; set; }
    }
}
