using Webjet.Backend.Products.Queries.GetProductsList;

namespace Webjet.Backend.Movies.GetMovieList;

public class MoviesListDto
{
    public required List<MovieDto> Movies { get; init; }
}