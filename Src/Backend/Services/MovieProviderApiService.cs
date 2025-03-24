using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Movies.GetMovieDetail;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Services;

public class MovieProviderApiService(
    IConfiguration config,
    ILogger<MovieProviderApiService> logger,
    IHttpClientFactory httpClientFactory) : IMovieProviderApiService
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
            var movies = JsonConvert.DeserializeObject<MoviesListDto>(result) ??
                         throw new InvalidDataException("Invalid data from External API");
            return movies;
        }

        throw new HttpRequestException("Failed to get movies list");
    }

    public async Task<MovieDetailDto> GetMovieDetails(MovieProvider movieProvider, string movieId)
    {
        var movieProviderApiConfig = GetConfig(movieProvider);
        var apiResponse = await GetAsync(movieProviderApiConfig, movieProviderApiConfig.GetMovie, movieId);
        if (apiResponse.IsSuccessStatusCode)
        {
            var result = await apiResponse.Content.ReadAsStringAsync();
            var movieDetails = JsonConvert.DeserializeObject<MovieDetailDto>(result) ??
                               throw new InvalidDataException("Invalid data from External API");
            return movieDetails;
        }

        throw new HttpRequestException($"Failed to get movie details: {movieProvider}/{movieId}");
    }

    private async Task<HttpResponseMessage> GetAsync(MovieProviderApiConfig movieProviderApiConfig, string apiRoute,
        string apiPath = "")
    {
        using var client = BuildHcpClient(movieProviderApiConfig);
        var uriString = $"{client.BaseAddress?.AbsoluteUri}/{apiRoute}" +
                        (string.IsNullOrEmpty(apiPath) ? string.Empty : $"/{apiPath}");
        client.BaseAddress = new Uri(uriString);
        logger.LogInformation($"Sending GET request to API endpoint: {uriString}");
        return await client.GetAsync("");
    }

    private HttpClient BuildHcpClient(MovieProviderApiConfig movieProviderApiConfig)
    {
        //var client = httpClientFactory.CreateClient();

        var client = new HttpClient(new HttpClientHandler());
        client.Timeout = TimeSpan.FromSeconds(movieProviderApiConfig.TimeoutSeconds);
        client.BaseAddress = new Uri(movieProviderApiConfig.BaseUrl);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonHeaderType));
        client.DefaultRequestHeaders.Add(XAccessTokenHeader, movieProviderApiConfig.AccessToken);
        return client;
    }

    private MovieProviderApiConfig? GetConfig(MovieProvider movieProvider)
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