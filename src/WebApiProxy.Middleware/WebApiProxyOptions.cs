namespace WebApiProxy.Middleware
{
    public abstract class WebApiProxyOptions
    {
        public string AbsoluteEndpoint
        {
            get
            {
                if (!Endpoint.StartsWith("/"))
                {
                    return $"/{Endpoint}";
                }

                return Endpoint;
            }
        }
        public abstract string Endpoint { get; set; }
        public virtual string HttpMethod { get; set; } = "GET";
    }
    
}
