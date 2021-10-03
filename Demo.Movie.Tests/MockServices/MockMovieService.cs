
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;

namespace Demo.Movie.Tests.MockServices
{
    public class MockMovieService : IMovieService
    {
        private const int _FREE_DUDE_ID = 123456,
                          _SPIDER_SPY_ID = 789101,
                          _MCDONALD_PALS_ID = 112131,
                          _BIG_TIME_ID = 345267;

        public MockMovieService() { }

        public Task<GenreResponse> GetAvailableGenres()
        {
            return Task.Run(() =>
            {
                return new GenreResponse
                {
                    genres = new List<Genre>()
                    {
                        new Genre()
                        {
                            id = 12,
                            name = "Adventure"
                        },
                        new Genre()
                        {
                            id = 16,
                            name = "Animation"
                        },
                        new Genre()
                        {
                            id = 35,
                            name = "Comedy"
                        },
                        new Genre()
                        {
                            id = 10751,
                            name = "Family"
                        }
                    }
                };
            });
        }

        public Task<ImageConfigurationResponse> GetImageConfiguration()
        {
            return Task.Run(() =>
            {
                return new ImageConfigurationResponse()
                {
                    images = new ImageConfiguration()
                    {
                        base_url = "https://test.baseurl.org/",
                        secure_base_url = "https://test.securebaseurl.org/",
                        poster_sizes = new List<string>()
                        {
                            "w123",
                            "w456",
                            "w789"
                        }
                    }
                };
            });
        }

        public Task<PopularFilmsResponse> GetMostRecentPopularFilms()
        {
            return Task.Run(() =>
            {
                return new PopularFilmsResponse()
                {
                    results = new List<Film>()
                    {
                        new Film()
                        {
                            id = _FREE_DUDE_ID,
                            adult = false,
                            genre_ids = new int[] { 35,28,12 },
                            title = "Free Dude",
                            poster_path = "/free_dude_test.jpg",
                            release_date = "2021-08-11",
                            overview = "This is a test overview for Free Dude.",
                            popularity = 5418,
                            vote_count = 1234,
                            vote_average = 7
                        },
                        new Film()
                        {
                            id = _SPIDER_SPY_ID,
                            adult = false,
                            genre_ids = new int[] { 12,10751 },
                            title = "Spider Spy",
                            poster_path = "/spider_spy_test.jpg",
                            release_date = "2021-02-23",
                            overview = "This is a test overview for Spider Spy.",
                            popularity = 6000,
                            vote_count = 567,
                            vote_average = 9
                        },
                        new Film()
                        {
                            id = _MCDONALD_PALS_ID,
                            adult = false,
                            genre_ids = new int[] { 12,16,10751 },
                            title = "Old McDonald Pals",
                            poster_path = "/old_mcdonald_pals_test.jpg",
                            release_date = "2021-08-27",
                            overview = "This is a test overview for Old McDonald Pals.",
                            popularity = 4789,
                            vote_count = 1002,
                            vote_average = 5
                        },
                        new Film()
                        {
                            id = _BIG_TIME_ID,
                            adult = false,
                            genre_ids = new int[] { 35,10751 },
                            title = "Big Time: The Movie",
                            poster_path = "/big_time_test.jpg",
                            release_date = "2021-08-27",
                            overview = "This is a test overview for Big Time: The Movie.",
                            popularity = 4500,
                            vote_count = 1002,
                            vote_average = 6
                        }
                    }
                };
            });
        }

        public Task<FilmTrailerResponse> GetTrailerInformationByFilmId(int id)
        {
            return Task.Run(() =>
            {
                switch(id)
                {
                    case _FREE_DUDE_ID:
                        return new FilmTrailerResponse()
                        {
                            results = new List<FilmTrailer>()
                            {
                                new FilmTrailer()
                                {
                                    name = "Free Dude Lives!",
                                    key = "FR33D463",
                                    site = "MeTube"
                                }
                            }
                        };

                    case _SPIDER_SPY_ID:
                        return new FilmTrailerResponse()
                        {
                            results = new List<FilmTrailer>()
                            {
                                new FilmTrailer()
                                {
                                    name = "Spy Again",
                                    key = "SPI634SP1",
                                    site = "MeTube"
                                }
                            }
                        };

                    case _MCDONALD_PALS_ID:
                        return new FilmTrailerResponse()
                        {
                            results = new List<FilmTrailer>()
                            {
                                new FilmTrailer()
                                {
                                    name = "Teaser Trailer",
                                    key = "MC00N410",
                                    site = "MeTube"
                                }
                            }
                        };

                    case _BIG_TIME_ID:
                        return new FilmTrailerResponse()
                        {
                            results = new List<FilmTrailer>()
                            {
                                new FilmTrailer()
                                {
                                    name = "Big Time Teaser Trailer",
                                    key = "B15T1831",
                                    site = "MeTube"
                                }
                            }
                        };

                    default:
                        return null;
                }

            });
        }
    }
}
