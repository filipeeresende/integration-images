using AspNetCoreRateLimit;

namespace BonifiQ.Application.Configurations
{
    public static class RateLimitConfg
    {
        public static IServiceCollection AddRateLimit(this IServiceCollection services)
        {
           
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules = new List<RateLimitRule>

        {
             
            new RateLimitRule
            {
                Endpoint = $"GET:*/v1/photo*",
                Period = "24h",
                Limit = 30,
            },
             new RateLimitRule
            {
                Endpoint = "GET:*/v1/album/*/photos",
                Period = "1m",
                Limit = 10,
            }
        };
            });

            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
