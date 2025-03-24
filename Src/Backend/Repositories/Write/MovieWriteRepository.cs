using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write
{
    public class MovieWriteRepository(Func<WebjetMoviesDbContext> context) : BaseWriteRepository(context), IMovieWriteRepository
    {
        public DbSet<MovieSummary> GetMovieSummaries()
        {
            return DbContext.MovieSummaries;
        }

        public async Task AddOrUpdateMovieSummary(MovieDto movieDto)
        {
            await Transact(async () =>
            {
                var existingMovieList = DbContext.MovieSummaries;

                var existingMovie = existingMovieList.SingleOrDefault(m => m.MovieId == movieDto.Id);
                if (existingMovie != null)
                {
                    existingMovie.Price = Convert.ToDecimal(movieDto.Price);
                    existingMovie.UpdatedAt = DateTimeOffset.Now;
                }
                else
                {
                    existingMovieList.Add(new MovieSummary
                    {
                        MovieId = movieDto.Id,
                        Poster = movieDto.Poster,
                        Title = movieDto.Title,
                        CreatedAt = DateTimeOffset.Now,
                        UpdatedAt = DateTimeOffset.Now,
                        Price = Convert.ToDecimal(movieDto.Price),
                    });
                }

                // TODO: deal with deleted movies or updated details
                await DbContext.SaveChangesAsync();
            });
        }

        public async Task AddMovieSummariesAsync(List<MovieDto> movieDtos)
        {
            await Transact(async () =>
            {
                var existingMovieList = DbContext.MovieSummaries;

                foreach (var movieDto in movieDtos)
                {
                    var existingMovie = existingMovieList.SingleOrDefault(m => m.MovieId == movieDto.Id);
                    if (existingMovie == null)
                    {
                        existingMovieList.Add(new MovieSummary
                        {
                            MovieId = movieDto.Id,
                            Poster = movieDto.Poster,
                            Title = movieDto.Title,
                            CreatedAt = DateTimeOffset.Now,
                            UpdatedAt = DateTimeOffset.Now,
                            Price = Convert.ToDecimal(movieDto.Price),
                        });
                    }
                    else existingMovie.Price = Convert.ToDecimal(movieDto.Price);
                    
                    // TODO: deal with deleted movies or updated details
                }
               
                await DbContext.SaveChangesAsync();
            });
        }


    }
}
