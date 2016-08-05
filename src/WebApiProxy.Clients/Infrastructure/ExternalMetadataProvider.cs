namespace WebApiProxy.Clients.Infrastructure
{
    using System.Net.Http;
    using Newtonsoft.Json;
    using Core.Infrastructure;
    using Core.Models;
    using Models;

    public class ExternalMetadataProvider : IMetadataProvider
    {
        private readonly ClientConfiguration configuration;

        public ExternalMetadataProvider(ClientConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Metadata GetMetadata(string baseUrl = "")
        {
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(new HttpRequestMessage(configuration.Method, configuration.MetadataEndpoint));
                var result = response.Result.Content.ReadAsStringAsync().Result;
                var metadata = JsonConvert.DeserializeObject<Metadata>(result);

                return metadata;
            }

        }
    }
}
