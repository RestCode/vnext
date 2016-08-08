namespace WebApiProxy.Clients.Models
{
    public class ClientConfiguration
    {
        public string MetadataEndpoint { get; set; }
        public string Method { get; set; } = "OPTIONS";
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Suffix { get; set; }
        public string Generator { get; set; }
    }
    
}
