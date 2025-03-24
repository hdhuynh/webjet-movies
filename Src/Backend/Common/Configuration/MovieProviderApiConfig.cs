namespace Webjet.Backend.Common.Configuration;

public record MovieProviderApiConfig(
    string BaseUrl,
    string GetMovies,
    string GetMovie,
    string AccessToken,
    int TimeoutSeconds);