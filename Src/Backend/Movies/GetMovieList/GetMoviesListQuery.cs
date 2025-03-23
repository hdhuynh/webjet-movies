using MediatR;
using Webjet.Backend.Repositories.Read;

namespace Webjet.Backend.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

public class GetMoviesListQueryHandler(IMovieReadRepository readRepository) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        var movieDtos = await readRepository.GetMovieSummaries();
        return new MoviesListVm
        {
            Movies = movieDtos.ToList()
        };
    }
}