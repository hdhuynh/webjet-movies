using MediatR;
using Webjet.Backend.Movies.GetMovieList;
using Webjet.Backend.Repositories.Read;

namespace Webjet.Backend.Movies.GetMovieDetail;

public record GetMovieDetailQuery(string movieId) : IRequest<MovieDetailVm>;

public class GetMovieDetailQueryHandler(IMovieReadRepository readRepository) : IRequestHandler<GetMovieDetailQuery, MovieDetailVm>
{
    public async Task<MovieDetailVm> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
    {
        var movieDetailVm = await readRepository.GetMovieDetails(request.movieId);
        return movieDetailVm;
    }
}