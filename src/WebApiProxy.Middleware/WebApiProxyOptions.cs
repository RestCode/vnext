namespace WebApiProxy.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class WebApiProxyOptions
    {
        //private readonly SwaggerGeneratorOptions _swaggerGeneratorOptions;
        
        public string MetadataEndpoint { get; set; } = "";
        public string HttpMethod { get; set; } = "OPTIONS";

        public Dictionary<Type, ClientProviderOptions> ClientProviderOptions { get; set; } = new Dictionary<Type, Middleware.ClientProviderOptions>();

        //public IServiceProvider Services { get; set; }
        public WebApiProxyOptions()
        {
            //_swaggerGeneratorOptions = new SwaggerGeneratorOptions();
            
        }
    }

    public class ClientProviderOptions
    {
        public string Endpoint { get; set; } = "";
        public string HttpMethod { get; set; } = "GET";
    }
}
