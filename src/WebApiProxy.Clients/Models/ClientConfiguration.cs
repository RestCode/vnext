using System.Net.Http;

namespace WebApiProxy.Clients.Models
{
    public class ClientConfiguration
    {
        public string MetadataEndpoint { get; set; }
        public HttpMethod Method { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Suffix { get; set; }
        public string Generator { get; set; }
    }
    
}
