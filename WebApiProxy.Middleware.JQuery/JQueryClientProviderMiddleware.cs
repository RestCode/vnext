using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProxy.Core.Infrastructure;
using WebApiProxy.Core.Models;
using WebApiProxy.Generators.JQuery;

namespace WebApiProxy.Providers.JQuery
{
    public class JQueryClientProviderMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ISwaggerProvider _swaggerProvider;
        //private readonly JsonSerializer _swaggerSerializer;
        //private readonly Action<HttpRequest, SwaggerDocument> _documentFilter;
        //private readonly TemplateMatcher _requestMatcher;

        public JQueryClientProviderMiddleware(
            RequestDelegate next,
            string routeTemplate = "jquery")
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            var configuration = new ClientConfiguration
            {

                ProxyUrl = "http://foo.com",
                
                
                
            };
            var meta = new Metadata()
            {
                Host = host
            };

            var provider = new DefaultMetadataProvider(meta);
            IGenerator generator = new JQueryGenerator(provider, configuration);


            var result = await generator.Process();

            
            string apiVersion;
            if (!requestingClient(httpContext.Request, configuration.Action))
            {
                await _next(httpContext);
                return;
            }

            await httpContext.Response.WriteAsync(result);

        }
        private bool requestingClient(HttpRequest request, string method = "GET")
        {
            if (request.Method == method && request.Path.Value == "/jquery")
            {
                return true;
            }
            return false;
        }

    }
}
