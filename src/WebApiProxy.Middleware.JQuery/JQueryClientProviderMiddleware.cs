namespace WebApiProxy.Providers.JQuery
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Generators.JQuery;
    using Core.Models;
    using Clients.Models;
    using Microsoft.Extensions.Options;
    using Middleware;
    using System;

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

    public class JQueryClientProviderMiddleware: IClientProvider
    {
        private readonly RequestDelegate _next;
        private readonly IMetadataProvider _metadataProvider;
        private readonly WebApiProxyOptions _options;

       

        //private readonly Action<HttpRequest, SwaggerDocument> _documentFilter;
        //private readonly TemplateMatcher _requestMatcher;

        public JQueryClientProviderMiddleware(
            RequestDelegate next,
            IMetadataProvider metadataProvider,
            IOptions<WebApiProxyOptions> options)
        {
            _next = next;
            _metadataProvider = metadataProvider;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            
            var configuration = new ClientConfiguration();

            if (!requestingClient(httpContext.Request))
            {
                await _next(httpContext);
                return;
            }
            //var meta = new ExternalMetadataProvider(new ClientConfiguration
            //{
            //    ProxyUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}"
            //});
            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var metadata = _metadataProvider.GetMetadata(host);
            IGenerator generator = new JQueryGenerator(metadata);


            var result = await generator.Process();

            
            string apiVersion;
            

            await httpContext.Response.WriteAsync(result);

        }
        private bool requestingClient(HttpRequest request)
        {
            var clientOptions = _options.ClientProviderOptions[typeof(JQueryClientProviderMiddleware)];


            if (request.Method.Equals(clientOptions.HttpMethod, StringComparison.OrdinalIgnoreCase) && request.Path == clientOptions.Endpoint)
            {
                return true;
            }
            return false;
        }

    }
}
