using Webjet.Backend.Services.Movies.GetMovieDetail;
using Webjet.Backend.Services.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write;

public interface IMovieWriteRepository
{
    Task AddOrUpdateMovieSummary(MovieDto movieDto, MovieDetailDto movieDetailDto);
}