namespace WebApiProxy.Core.Models
{
    using System.Collections.Generic;
    public class Metadata
    {
        public Metadata()
        {
            Definitions = new List<ControllerDefinition>();
            Models = new List<ModelDefinition>();
        }
        public string Host { get; set; }

        public IEnumerable<ControllerDefinition> Definitions { get; set; }

        public IEnumerable<ModelDefinition> Models { get; set; }

    }
}
