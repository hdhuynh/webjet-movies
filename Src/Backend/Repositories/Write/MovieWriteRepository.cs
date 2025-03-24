using Webjet.Backend.Models.Data;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write
{
    public class MovieWriteRepository(Func<WebjetMoviesDbContext> context) : BaseWriteRepository(context), IMovieWriteRepository
    {
        public async Task AddOrUpdateMovieSummary(MovieDto movieDto, MovieDetailsDto movieDetailsDto)
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
                    });

                    var movieDetail = new MovieDetail
                    {
                        MovieId = movieDto.Id,
                        Plot = movieDetailsDto.Plot,
                        Genre = movieDetailsDto.Genre,
                        Director = movieDetailsDto.Director,
                        Actors = movieDetailsDto.Actors,
                        Released = movieDetailsDto.Released,
                        Runtime = movieDetailsDto.Runtime,
                        Language = movieDetailsDto.Language,
                        Country = movieDetailsDto.Country,
                        Awards = movieDetailsDto.Awards,
                        Rating = Convert.ToDecimal(movieDetailsDto.Rating),
                        Votes = movieDetailsDto.Votes,
                        Type = movieDetailsDto.Type,
                        UpdatedAt = DateTimeOffset.Now,
                    };
                   
                    if (decimal.TryParse(movieDetailsDto.Rating, out var rating))
                    {
                        movieDetail.Rating = rating;
                    }
                    if (short.TryParse(movieDetailsDto.Metascore, out var metaScore))
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
