namespace Webjet.Backend.Movies.GetMovieList;

public class MoviesListVm
{
    public required IEnumerable<MovieDto> Movies { get; init; }
}