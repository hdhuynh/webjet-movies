using MediatR;
using Webjet.Backend.Repositories.Read;

namespace Webjet.Backend.Services.Movies.GetMovieDetail;

public record GetMovieDetailQuery(string MovieId) : IRequest<MovieDetailVm>;

public class GetMovieDetailQueryHandler(IMovieReadRepository readRepository) : IRequestHandler<GetMovieDetailQuery, MovieDetailVm>
{
    public async Task<MovieDetailVm> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
    {
        var movieDetailVm = await readRepository.GetMovieDetails(request.MovieId);
        return movieDetailVm;
    }
}