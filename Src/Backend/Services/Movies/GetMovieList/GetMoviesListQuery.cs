using MediatR;
using Webjet.Backend.Repositories.Read;

namespace Webjet.Backend.Services.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

public class GetMoviesListQueryHandler(IMovieReadRepository readRepository) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        //TODO: If data is unlikely to change, we can cache the data here (can be done by using Redis).
        //      When the data is changed by background job, we can invalidate the cache

        var movieDtos = await readRepository.GetMovieSummaries();

        return new MoviesListVm
        {
            Movies = movieDtos
        };
    }
}