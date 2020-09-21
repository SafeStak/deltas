using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SafeStak.Deltas.WebApi.AdaPools;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using SafeStak.Deltas.WebApi.Middleware;

namespace SafeStak.Deltas.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var poolApiSettings = _configuration.GetSection("PoolApiSettings").Get<PoolApiSettings>();

            services.AddControllers();

            services.AddHealthChecks();

            services.AddSingleton(poolApiSettings);

            services.AddHttpClient<IAdaPoolsApiClient, AdaPoolsApiClient>(
                nameof(AdaPoolsApiClient),
                client => client.BaseAddress = new Uri(poolApiSettings.BaseUrl));

            services
               .AddSingleton<IPoolStatsRetriever, AdaPoolsPoolStatsRetriever>()
               .AddSingleton<IPoolStatsSerialiser, PoolStatsPrometheusMetricsSerialiser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // To accommodate nginx proxy header forwarding 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            
            app.UseMiddleware<EnrichResponseHeadersMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });            
        }
    }
}
