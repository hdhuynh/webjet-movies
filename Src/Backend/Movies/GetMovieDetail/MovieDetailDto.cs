using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Movies.GetMovieDetail;

public class MovieDetailDto: MovieDto
{
    public required string Year { get; init; }

    public required string Rated { get; init; }

    public required string Released { get; init; }

    public required string Runtime { get; init; }

    public required string Genre { get; init; }

    public required string Director { get; init; }

    public required string Writer { get; init; }

    public required string Actors { get; init; }

    public required string Plot { get; init; }

    public required string Language { get; init; }

    public required string Country { get; init; }

    public required string Awards { get; init; }

    public required string Metascore { get; init; }

    public required string Rating { get; init; }

    public required string Votes { get; init; }

    public required string Type { get; init; }
}