using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Services;

public interface IMovieProviderApiService
{
    Task<MoviesListDto> GetAllMovies(MovieProvider movieProvider);
}
public class MovieProviderApiService(IConfiguration config, ILogger<MovieProviderApiService> logger, IHttpClientFactory httpClientFactory) : IMovieProviderApiService
{
    private const string XAccessTokenHeader = "x-access-token";
    private const string ApplicationJsonHeaderType = "application/json";

    public async Task<MoviesListDto> GetAllMovies(MovieProvider movieProvider)
    {
        var movieProviderApiConfig = GetConfig(movieProvider);
        var apiResponse = await GetAsync(movieProviderApiConfig, movieProviderApiConfig.GetMovies);
        if (apiResponse.IsSuccessStatusCode)
        {
            var result = await apiResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<MoviesListDto>(result) ?? throw new InvalidDataException("Invalid data from External API");

            //TODO: validate the data before returning
            return movies;
        }
        throw new HttpRequestException("Failed to get movies list");
    }

    public async Task<HttpResponseMessage> GetAsync(MovieProviderApiConfig config, string apiRoute, string queryString = "")
	{
		using var client = BuildHcpClient(config);
		client.BaseAddress = new Uri($"{client.BaseAddress?.AbsoluteUri}/{apiRoute}");
        logger.LogInformation("Sending GET request to external API endpoint");
		return await client.GetAsync(queryString);
	}

	private HttpClient BuildHcpClient(MovieProviderApiConfig config)
    {
        var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(config.BaseUrl);
        client.DefaultRequestHeaders.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonHeaderType));
		client.DefaultRequestHeaders.Add(XAccessTokenHeader, config.AccessToken);
		return client;
	}

    private MovieProviderApiConfig GetConfig(MovieProvider movieProvider)
    {
        try
        {
            return config.GetSection(GetConfigSectionName(movieProvider)).Get<MovieProviderApiConfig>();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get configuration for {MovieProvider}", movieProvider);
            throw;
        }
    }

    private string GetConfigSectionName(MovieProvider movieProvider)
    {
        return movieProvider switch
        {
            MovieProvider.CinemaWorld => "ExternalAPIs:CinemaWorld",
            MovieProvider.FilmWorld => "ExternalAPIs:FilmWorld",
            _ => throw new ArgumentOutOfRangeException(nameof(movieProvider), movieProvider, null)
        };
    }
}