namespace WebApiProxy.Middleware
{
    public class WebApiProxyProviderOptions : WebApiProxyOptions
    {
        public override string Endpoint { get; set; } = "";
        public override string HttpMethod { get; set; } = "OPTIONS";

    }
}
