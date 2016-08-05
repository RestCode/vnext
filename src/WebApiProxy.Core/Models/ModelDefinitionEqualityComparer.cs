namespace WebApiProxy.Core.Models
{
    using System;
    using System.Collections.Generic;
    public class ModelDefinitionEqualityComparer : IEqualityComparer<ModelDefinition>
    {
        public bool Equals(ModelDefinition x, ModelDefinition y)
        {
            return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(ModelDefinition obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
