using BonifiQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BonifiQ.Application.Configurations
{
    public static class ContextConfig
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BonifiQContext>(options => options
           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }
    }
}
