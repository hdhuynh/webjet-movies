namespace Webjet.Backend.Movies.GetMovieDetail;

public class MovieDetailVm
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required string Poster { get; init; }

    public required string Price { get; init; }

    public required string BestPriceProvider { get; init; }

    public short? Year { get; init; }

    public string Rated { get; init; }

    public string Released { get; init; }

    public string Runtime { get; init; }

    public string Genre { get; init; }

    public string Director { get; init; }

    public string Writer { get; init; }

    public string Actors { get; init; }

    public string Plot { get; init; }

    public string Language { get; init; }

    public string Country { get; init; }

    public string Awards { get; init; }

    public short? Metascore { get; init; }

    public decimal? Rating { get; init; }

    public string Votes { get; init; }

    public string Type { get; init; }
   
}