using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using WebApiProxy.Providers.JQuery;

namespace Microsoft.AspNetCore.Builder
{
    public static class JQueryProviderBuilderExtensions
    {
        public static IApplicationBuilder UseJQueryClientProvider(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<JQueryClientProviderMiddleware>();
        }
        
    }
}
