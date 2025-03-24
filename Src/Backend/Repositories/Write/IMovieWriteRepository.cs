using Webjet.Backend.Movies.GetMovieDetail;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Write;

public interface IMovieWriteRepository
{
    Task AddOrUpdateMovieSummary(MovieDto movieDto, MovieDetailDto movieDetailDto);
}