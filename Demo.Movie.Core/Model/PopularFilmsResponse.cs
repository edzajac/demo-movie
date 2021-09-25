using System;
using System.Collections.Generic;

namespace Demo.Movie.Core.Model
{
    public class PopularFilmsResponse
    {
        public int page { get; set; }
        public IEnumerable<Film> results { get; set; }
    }
}
