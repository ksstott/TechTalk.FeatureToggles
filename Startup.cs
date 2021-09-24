namespace TechTalk.FeatureToggles
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.FeatureManagement;
    using Microsoft.FeatureManagement.FeatureFilters;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TechTalk.FeatureToggles.Auth;
    using TechTalk.FeatureToggles.Controllers;
    using TechTalk.FeatureToggles.FeatureToggles;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddAzureAppConfiguration();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechTalk.FeatureToggles", Version = "v1" });
            });
            services.AddFeatureManagement()
                    .AddFeatureFilter<PercentageFilter>()
                    .AddFeatureFilter<TargetingFilter>()
                    .AddSessionManager<QueryParameterToggleManager>()
                    .AddSessionManager<AllTogglesEnabledManager>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddSingleton<ITargetingContextAccessor, TargetingContextAccessor>();

            services.AddTransient<IWeatherServiceEnhancer, WeatherSummaryEnhancer>();

            services.AddAuthentication("User").AddScheme<AuthenticationSchemeOptions, HeaderAuthHandler>("User", opt => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAzureAppConfiguration();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechTalk.FeatureToggles v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
