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
            var c = WebRequest.CreateHttp(configuration.MetadataEndpoint);
            c.Method = configuration.Method;
            var response = c.GetResponseAsync().Result;
           // var result = response.
          //  using (var client = new HttpClient())
            {
            //    var response = client.SendAsync(new HttpRequestMessage(new HttpMethod(configuration.Method), configuration.MetadataEndpoint));
                var result = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(result))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JsonSerializer ser = new JsonSerializer();
                    return ser.Deserialize<Metadata>(jsonReader);
                }
            }

        }
    }
}
