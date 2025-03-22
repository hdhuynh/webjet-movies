using Webjet.Backend.Products.Queries.GetProductsList;

namespace Webjet.Backend.Movies.GetMovieList;

public class MoviesListVm
{
    public required IList<MovieDto> Movies { get; init; }
}