using AspNetCoreRateLimit;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Services;
using BonifiQ.Infrastructure.CrossTalk;
using BonifiQ.Infrastructure.Repositories;

namespace BonifiQ.Application.Configurations
{
    public static class DependecyConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            //Repositories
            services.AddScoped<IPhotosRepository, PhotosRepository>();

            //Services
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IAlbumService, AlbumService>();

            // chamadas
            services.AddScoped<IImageRequests, ImageRequests>();

            return services;
        }
    }
}
