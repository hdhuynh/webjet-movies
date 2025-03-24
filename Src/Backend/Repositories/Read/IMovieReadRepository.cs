using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Read;

public interface IMovieReadRepository
{
    Task<IEnumerable<MovieDto>> GetMovieSummaries();
    Task<MovieDetailVm> GetMovieDetails(string movieId);
}