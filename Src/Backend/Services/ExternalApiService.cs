using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;

namespace Webjet.Backend.Services;

public interface IExternalApiService
{
	Task<HttpResponseMessage> GetAsync(string apiRoute, string queryString);
	Task<HttpResponseMessage> PostAsync(string apiRoute, string queryString, HttpContent content);
}
public class ExternalApiService(ILogger<ExternalApiService> logger, IConfiguration config) : IExternalApiService
{
    public async Task<HttpResponseMessage> GetAsync(string apiRoute, string queryString)
	{
		using var client = BuildHcpClient(config);
		client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + apiRoute);
        logger.LogInformation("Sending GET request to external API endpoint");
		return await client.GetAsync(queryString);
	}

	public async Task<HttpResponseMessage> PostAsync(string apiRoute, string queryString, HttpContent content)
	{
		using var client = BuildHcpClient(config);
		client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + apiRoute);
        logger.LogInformation("Sending POST request to external API endpoint");
		return await client.PostAsync(queryString, content);
	}

	private static HttpClient BuildHcpClient(IConfiguration config)
	{
        //var baseUrl = config.GetSection("ExternalAPIs").GetSection("CinemaWorld")..GetValue<string>("SomnoMedIntegrationAPI:BaseUrl");

  //       var baseUrl = PIConfiguration.Current.GetValue<string>("SomnoMedIntegrationAPI:BaseUrl");
		// var userName = PIConfiguration.Current.GetValue<string>("SomnoMedIntegrationAPI:Username");
		// var password = PIConfiguration.Current.GetValue<string>("SomnoMedIntegrationAPI:Password");
        var baseUrl = "https://webjetapitest.azurewebsites.net/api/cinemaworld";
        var accessToken = "sjd1HfkjU83ksdsm3802k";

		var clientHandler = new HttpClientHandler();
		clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

		var client = new HttpClient(clientHandler)
		{
			BaseAddress = new Uri(baseUrl)
		};
		client.DefaultRequestHeaders.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		client.DefaultRequestHeaders.Add("x-access-token", accessToken);
		return client;
	}

	private static string BuildBasicEncoding(string username, string password)
	{
		return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{username}:{password}"));
	}
}