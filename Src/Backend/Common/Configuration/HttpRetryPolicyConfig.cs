using Polly;
using Polly.Extensions.Http;

namespace Webjet.Backend.Common.Configuration
{
    public static class HttpRetryPolicyConfig
    {
        /// <summary>
        /// Retry policy for HttpClient. Ref: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicyConfig()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => !msg.IsSuccessStatusCode)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}