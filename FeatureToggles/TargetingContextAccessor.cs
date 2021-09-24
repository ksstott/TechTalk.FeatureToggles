namespace TechTalk.FeatureToggles.FeatureToggles
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.FeatureManagement.FeatureFilters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TargetingContextAccessor : ITargetingContextAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TargetingContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;

            TargetingContext targetingContext = new TargetingContext
            {
                UserId = httpContext.User.Identity.Name
            };
            return new ValueTask<TargetingContext>(targetingContext);
        }
    }
}
