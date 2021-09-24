using Microsoft.FeatureManagement;
using System;
using System.Threading.Tasks;

namespace TechTalk.FeatureToggles
{
    public interface IWeatherServiceEnhancer
    {
        Task Enhance(WeatherForecast weatherForecast);
    }

    public class WeatherSummaryEnhancer : IWeatherServiceEnhancer
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IFeatureManager featureManager;

        public WeatherSummaryEnhancer(IFeatureManager featureManager)
        {
            this.featureManager = featureManager;
        }

        public async Task Enhance(WeatherForecast weatherForecast)
        {
           weatherForecast.Summary = await GetSummary();
        }

        private static Task<string> GetSummary()
        {
            var rng = new Random();
            return Task.FromResult(Summaries[rng.Next(Summaries.Length+1)]);
        }
    }
}
