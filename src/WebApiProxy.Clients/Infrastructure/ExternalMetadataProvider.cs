namespace WebApiProxy.Clients.Infrastructure
{
    using Newtonsoft.Json;
    using Core.Infrastructure;
    using Core.Models;
    using Models;
    using System.Net;
    using System.IO;

    public class ExternalMetadataProvider : IMetadataProvider
    {
        private readonly ClientConfiguration configuration;

        public ExternalMetadataProvider(ClientConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public Metadata GetMetadata(string baseUrl = "")
        {
            var client = WebRequest.CreateHttp(configuration.MetadataEndpoint);
            client.Method = configuration.Method;
            var response = client.GetResponseAsync().Result;

            var result = response.GetResponseStream();
            using (var reader = new StreamReader(result))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();
                return ser.Deserialize<Metadata>(jsonReader);
            }
        }
    }
}
