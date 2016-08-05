namespace WebApiProxy.Core.Models
{
    public class ClientConfiguration
    {
        public string ProxyUrl { get; set; }
        public string Action { get; set; } = "OPTIONS";
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Suffix { get; set; }
        public string Generator { get; set; }
    }
    
}
