using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTalk.FeatureToggles.Controllers
{
    public interface IWeatherService
    {
        IAsyncEnumerable<WeatherForecast> Get(int days);
    }

    public class WeatherService : IWeatherService
    {
        private readonly IFeatureManager featureManager;

        public WeatherService(IFeatureManager featureManager)
        {
            this.featureManager = featureManager;
        }

        public async IAsyncEnumerable<WeatherForecast> Get(int days)
        {
            foreach (var item in Enumerable.Range(0, days))
            {
                var tempteratureC = item * 10;
                yield return new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(item).Date,
                    TemperatureC = tempteratureC,
                };
            }
        }

        //public async IAsyncEnumerable<WeatherForecast> Get(int days)
        //{
        //    var temperatureFEnabled = await this.featureManager.IsEnabledAsync("TemperatureF");
        //    foreach (var item in Enumerable.Range(0, days))
        //    {
        //        if (temperatureFEnabled)
        //        {
        //            var tempteratureC = item * 10;
        //            yield return new WeatherForecast
        //            {
        //                Date = DateTime.Now.AddDays(item).Date,
        //                TemperatureC = tempteratureC,
        //                TemperatureF = await ConvertToFahrenheit(tempteratureC),
        //            };
        //        }
        //    }
        //}

        //public async IAsyncEnumerable<WeatherForecast> Get(int days)
        //{
        //    var temperatureFEnabled = await this.featureManager.IsEnabledAsync("TemperatureF");
        //    foreach (var item in Enumerable.Range(0, days))
        //    {
        //        var tempteratureC = item * 10;
        //        yield return new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(item).Date,
        //            TemperatureC = tempteratureC,
        //            TemperatureF = temperatureFEnabled ? await ConvertToFahrenheit(tempteratureC) : null
        //        };
        //    }
        //}

        //public async IAsyncEnumerable<WeatherForecast> Get(int days)
        //{
        //    var temperatureFEnabled = await this.featureManager.IsEnabledAsync("TemperatureF");
        //    foreach (var item in Enumerable.Range(0, days))
        //    {
        //        var tempteratureC = item * 10;
        //        yield return new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(item).Date,
        //            TemperatureC = tempteratureC,
        //            TemperatureF = temperatureFEnabled ? await ConvertToFahrenheit2(tempteratureC) : null
        //        };
        //    }
        //}

        //private readonly IEnumerable<IWeatherServiceEnhancer> weatherServiceEnhancers;

        //public WeatherService(IEnumerable<IWeatherServiceEnhancer> weatherServiceEnhancers)
        //{
        //    this.weatherServiceEnhancers = weatherServiceEnhancers;
        //}

        //public async IAsyncEnumerable<WeatherForecast> Get(int days)
        //{
        //    foreach (var item in Enumerable.Range(0, days))
        //    {
        //        var tempteratureC = item * 10;
        //        var forecast = new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(item).Date,
        //            TemperatureC = tempteratureC,
        //            TemperatureF = await ConvertToFahrenheit2(tempteratureC)
        //        };

        //        await Task.WhenAll(this.weatherServiceEnhancers.Select(async e => await e.Enhance(forecast)));

        //        yield return forecast;
        //    }
        //}

        private static async Task<int> ConvertToFahrenheit(int tempteratureC)
        {
            await Task.Delay(100);
            return 32 + (int)(tempteratureC / 0.5556);
        }

        private static Task<int> ConvertToFahrenheit2(int tempteratureC)
        {
            return Task.FromResult(32 + (int)(tempteratureC / 0.5556));
        }
    }
}