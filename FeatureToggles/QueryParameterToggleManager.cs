using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace TechTalk.FeatureToggles
{
    public class QueryParameterToggleManager : ISessionManager
    {
        private readonly IHttpContextAccessor httpContextAssessor;

        public QueryParameterToggleManager(IHttpContextAccessor httpContextAssessor)
        {
            this.httpContextAssessor = httpContextAssessor;
        }

        public Task<bool?> GetAsync(string featureName)
        {
            return Task.FromResult<bool?>(this.httpContextAssessor.HttpContext.Request.Query.ContainsKey(featureName) ? true : null);
        }

        public Task SetAsync(string featureName, bool enabled)
        {
            return Task.CompletedTask;
        }
    }
}