namespace WebApiProxy.Core.Models
{
    using System.Collections.Generic;

    public class ControllerDefinition
	{
		public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ActionMethodDefinition> ActionMethods { get; set; }
 
    }
}
