using AutoMapper;
using Webjet.Backend.Common.Mappings;

namespace Webjet.Backend.Movies.GetMovieList;

public class MovieVm: IMapFrom<MovieDto>
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required string Poster { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MovieDto, MovieVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.Poster, opt => opt.MapFrom(s => s.Poster));
    }
}