using Webjet.Domain.Common.Base;

namespace Webjet.Domain.Movies;

public readonly record struct MovieId(int Value);

public class Movie(string movieID, string title, string poster) : BaseEntity<MovieId>
{
    public string MovieID { get; } = movieID;
    public string Title { get; } = title;
    public string Poster { get; } = poster;
}