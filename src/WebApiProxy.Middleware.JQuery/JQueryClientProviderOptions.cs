namespace WebApiProxy.Providers.JQuery
{
    using Middleware;
    public class JQueryClientProviderOptions : WebApiProxyOptions
    {
        public override string Endpoint { get; set; } = "/client.jquery.js";
    }
}
