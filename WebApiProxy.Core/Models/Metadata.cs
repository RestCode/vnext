using System.Collections.Generic;

namespace WebApiProxy.Core.Models
{
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
