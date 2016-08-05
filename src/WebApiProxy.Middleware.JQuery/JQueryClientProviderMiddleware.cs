namespace WebApiProxy.Providers.JQuery
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Generators.JQuery;
    using Clients.Infrastructure;
    using Core.Models;

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
            string routeTemplate = "jquery.js")
        {
            _next = next;
            _metadataProvider = metadataProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var configuration = new ClientConfiguration
            {

                ProxyUrl = "http://foo.com",

                Action = "GET"

            };
            if (!requestingClient(httpContext.Request, configuration.Action))
            {
                await _next(httpContext);
                return;
            }
            //var meta = new ExternalMetadataProvider(new ClientConfiguration
            //{
            //    ProxyUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}"
            //});
            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var metadata = _metadataProvider.GetMetadata();
            IGenerator generator = new JQueryGenerator(metadata, configuration);


            var result = await generator.Process();

            
            string apiVersion;
            

            await httpContext.Response.WriteAsync(result);

        }
        private bool requestingClient(HttpRequest request, string method = "GET")
        {
            if (request.Method == method && request.Path.Value == "/jquery.js")
            {
                return true;
            }
            return false;
        }

    }
}
