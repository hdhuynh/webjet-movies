using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Services;

namespace Webjet.Backend.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

public class GetMoviesListQueryHandler(IWebjetDbContext context, IMapper mapper, IExternalApiService externalApiService, IConfiguration config) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        var cinemaWorldConfig = config.GetSection("ExternalAPIs:CinemaWorld").Get<ExternalApiConfig>();
        var apiResponse = await externalApiService.GetAsync(cinemaWorldConfig!, cinemaWorldConfig!.GetMovies);
        if (apiResponse.IsSuccessStatusCode)
        {
            var result = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            var movies = JsonConvert.DeserializeObject<MoviesListVm>(result) ?? throw new InvalidDataException("Invalid data from External API");

            //TODO: validate the data before returning
            return movies;
        }

        throw new HttpRequestException("Failed to get movies list");
    }
}