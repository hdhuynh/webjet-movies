using Webjet.Application.Products.Queries.GetProductsList;

namespace Webjet.Application.Movies.GetMovieList;

public class MoviesListVm
{
    public required IList<MovieDto> Movies { get; init; }
}