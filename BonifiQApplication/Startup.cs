using AspNetCoreRateLimit;
using BonifiQ.Application.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BonifiQ.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveDependencies();
            services.AddWebApiConfiguration(MyAllowSpecificOrigins);
            services.AddDatabaseContext(Configuration);
            services.AddSwaggerConfig();
            services.AddMvc();
            services.AddRateLimit();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseIpRateLimiting();
            app.UseWebApiConfiguration(MyAllowSpecificOrigins);
            app.UseSwaggerConfig(env, provider, "");
          
        }
    }
}
