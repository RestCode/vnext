using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var generator = new JQueryGenerator();
            generator.Metadata = new Core.Models.Metadata
            {
                Host = "http://foo.bar",
                Definitions = new List<ControllerDefinition>(),
                Models = new List<ModelDefinition>()
            };

            var result = generator.TransformText();

            
            string apiVersion;
            if (!requestingClient(httpContext.Request))
            {
                await _next(httpContext);
                return;
            }

            await httpContext.Response.WriteAsync(result);

        }
        private bool requestingClient(HttpRequest request)
        {
            if (request.Method == "GET" && request.Path.Value == "/jquery")
            {
                return true;
            }
            return false;
        }

    }
}
