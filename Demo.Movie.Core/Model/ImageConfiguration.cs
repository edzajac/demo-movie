
using System.Collections.Generic;

namespace Demo.Movie.Core.Model
{
    public class ImageConfiguration
    {
        public string base_url { get; set; }
        public string secure_base_url { get; set; }
        public IEnumerable<string> poster_sizes { get; set; }
    }
}
