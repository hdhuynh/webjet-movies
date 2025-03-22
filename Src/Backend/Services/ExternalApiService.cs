using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Webjet.Backend.Common.Configuration;

namespace Webjet.Backend.Services;

public interface IExternalApiService
{
	Task<HttpResponseMessage> GetAsync(ExternalApiConfig config, string apiRoute, string queryString = "");
}
public class ExternalApiService(ILogger<ExternalApiService> logger) : IExternalApiService
{
    private const string XAccessTokenHeader = "x-access-token";
    private const string ApplicationJsonHeaderType = "application/json";

    public async Task<HttpResponseMessage> GetAsync(ExternalApiConfig config, string apiRoute, string queryString)
	{
		using var client = BuildHcpClient(config);
		client.BaseAddress = new Uri($"{client.BaseAddress?.AbsoluteUri}/{apiRoute}");
        logger.LogInformation("Sending GET request to external API endpoint");
		return await client.GetAsync(queryString);
	}

	private static HttpClient BuildHcpClient(ExternalApiConfig config)
	{
        var client = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, cert, _, _) => true
        });
        client.BaseAddress = new Uri(config.BaseUrl);
        client.DefaultRequestHeaders.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonHeaderType));
		client.DefaultRequestHeaders.Add(XAccessTokenHeader, config.AccessToken);
		return client;
	}
}