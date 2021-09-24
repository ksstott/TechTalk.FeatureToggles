using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace TechTalk.FeatureToggles
{
    public class AllTogglesEnabledManager : ISessionManager
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<AllTogglesEnabledManager> logger;

        public AllTogglesEnabledManager(IConfiguration configuration, ILogger<AllTogglesEnabledManager> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public Task<bool?> GetAsync(string featureName)
        {
            var allTogglesEnabled = this.configuration.GetValue<bool?>("AllTogglesEnabled");
            return Task.FromResult<bool?>(allTogglesEnabled);
        }

        public Task SetAsync(string featureName, bool enabled)
        {
            return Task.CompletedTask;
        }
    }
}