using Webjet.Backend.Services.Movies.GetMovieDetail;
using Webjet.Backend.Services.Movies.GetMovieList;

namespace Webjet.Backend.Repositories.Read;

public interface IMovieReadRepository
{
    Task<IEnumerable<MovieDto>> GetMovieSummaries();
    Task<MovieDetailVm> GetMovieDetails(string movieId);
}