using Webjet.Backend.Models.Data;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write
{
    public class MovieWriteRepository(Func<WebjetMoviesDbContext> context) : BaseWriteRepository(context), IMovieWriteRepository
    {
        public async Task AddOrUpdateMovieSummary(MovieDto movieDto, MovieDetailDto movieDetailDto)
        {
            await Transact(async () =>
            {
                var movieSummaries = DbContext.MovieSummaries;
                var movieDetails = DbContext.MovieDetails;

                var existingMovie = movieSummaries.SingleOrDefault(m => m.MovieId == movieDto.Id);
                if (existingMovie != null)
                {
                    existingMovie.Price = Convert.ToDecimal(movieDto.Price);
                    existingMovie.UpdatedAt = DateTimeOffset.Now;
                }
                else
                {
                    movieSummaries.Add(new MovieSummary
                    {
                        MovieId = movieDto.Id,
                        Poster = movieDto.Poster,
                        Title = movieDto.Title,
                        CreatedAt = DateTimeOffset.Now,
                        UpdatedAt = DateTimeOffset.Now,
                        Price = Convert.ToDecimal(movieDto.Price),
                        BestPriceProvider = movieDto.BestPriceProvider
                    });

                    var movieDetail = new MovieDetail
                    {
                        MovieId = movieDto.Id,
                        Plot = movieDetailDto.Plot,
                        Genre = movieDetailDto.Genre,
                        Director = movieDetailDto.Director,
                        Actors = movieDetailDto.Actors,
                        Released = movieDetailDto.Released,
                        Runtime = movieDetailDto.Runtime,
                        Language = movieDetailDto.Language,
                        Country = movieDetailDto.Country,
                        Awards = movieDetailDto.Awards,
                        Rating = Convert.ToDecimal(movieDetailDto.Rating),
                        Votes = movieDetailDto.Votes,
                        Type = movieDetailDto.Type,
                        UpdatedAt = DateTimeOffset.Now,
                    };
                   
                    if (decimal.TryParse(movieDetailDto.Rating, out var rating))
                    {
                        movieDetail.Rating = rating;
                    }
                    if (short.TryParse(movieDetailDto.Metascore, out var metaScore))
                    {
                        movieDetail.Metascore = metaScore;
                    }
                    movieDetails.Add(movieDetail);
                }

                // TODO: deal with deleted movies or updated details
                await DbContext.SaveChangesAsync();
            });
        }
    }
}
