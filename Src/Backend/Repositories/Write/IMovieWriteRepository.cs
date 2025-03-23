using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write;

public interface IMovieWriteRepository
{
    DbSet<MovieSummary> GetMovieSummaries();
    Task AddMovieSummariesAsync(List<MovieDto> movieDtos);
    Task AddOrUpdateMovieSummary(MovieDto movieDto);
}