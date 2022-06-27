using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Authentication.POC.API.APIKey.Common
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;

        public RateLimitingMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var decorator = endpoint?.Metadata.GetMetadata<LimitRequests>();
            if (decorator is null)
            {
                await _next(context);
                return;
            }

            var key = GenerateClientKey(context);
            var clientStatistics = await GetClientStatisticsByKey(key);
            if (clientStatistics != null &&
                   DateTime.UtcNow < clientStatistics.LastSuccessfulResponseTime.AddSeconds(decorator.TimeWindow) &&
                   clientStatistics.NumberOfRequestsCompletedSuccessfully == decorator.MaxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                return;
            }

            //await UpdateClientStatisticsStorage(key, decorator.MaxRequests);
            await _next(context);
        }

        private static string GenerateClientKey(HttpContext context) => 
            $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";

        private async Task<ClientStatistics?> GetClientStatisticsByKey(string key)
        { 
            var result = await _cache.GetAsync(key);

            if (result is null)
                return null;
            
            return JsonConvert.DeserializeObject<ClientStatistics>(Encoding.Default.GetString(result));
        }
    }

    public class ClientStatistics
    {
        public DateTime LastSuccessfulResponseTime { get; set; }
        public int NumberOfRequestsCompletedSuccessfully { get; set; }
    }
}
