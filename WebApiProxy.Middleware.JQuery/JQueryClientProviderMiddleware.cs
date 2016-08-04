using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProxy.Core.Infrastructure;
using WebApiProxy.Core.Models;
using WebApiProxy.Generators.JQuery;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApiProxy.Providers.JQuery
{
    //public class WebApiProxyProvider: IMetadataProvider
    //{
    //    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionsProvider;
    //    public WebApiProxyProvider(IApiDescriptionGroupCollectionProvider apiDescriptionsProvider)
    //    {
    //        this._apiDescriptionsProvider = apiDescriptionsProvider;
    //    }

    //    public Metadata GetMetadata()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private IEnumerable<ApiDescription> GetApiDescriptionsFor(string apiVersion)
    //    {
    //        var allDescriptions = _apiDescriptionsProvider.ApiDescriptionGroups.Items
    //            .SelectMany(group => group.Items);

    //        return allDescriptions;
    //    }
    //}

    public class JQueryClientProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetadataProvider _metadataProvider;
        //private readonly JsonSerializer _swaggerSerializer;
        //private readonly Action<HttpRequest, SwaggerDocument> _documentFilter;
        //private readonly TemplateMatcher _requestMatcher;

        public JQueryClientProviderMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            string routeTemplate = "jquery")
        {
            _next = next;
            _metadataProvider = metadataProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            var configuration = new ClientConfiguration
            {

                ProxyUrl = "http://foo.com",
                
                Action = "GET"
                
            };
            
            IGenerator generator = new JQueryGenerator(_metadataProvider, configuration);


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
