namespace Webjet.Backend.Movies.GetMovieList;

public class MovieDto
{
    public required string Id { get; set; }

    public required string Title { get; init; }

    public required string Poster { get; init; }

    public required string Price { get; set; }

    public required string BestPriceProvider { get; set; }
}