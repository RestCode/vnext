using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace WebApiProxy.Core.Infrastructure
{
    public static class Mapper
    {
        public static T Map<T>(this ExpandoObject source)
        {
            var propertyMap =
               typeof(T)
               .GetProperties()
               .ToDictionary(
                   p => p.Name.ToLower(),
                   p => p
               );
            var destination = Activator.CreateInstance<T>();
            // Might as well take care of null references early.
            if (source == null)
                throw new ArgumentNullException("source");
            //if (destination == null)
            //    throw new ArgumentNullException("destination");

            // By iterating the KeyValuePair<string, object> of
            // source we can avoid manually searching the keys of
            // source as we see in your original code.
            foreach (var kv in source)
            {
                PropertyInfo p;
                if (propertyMap.TryGetValue(kv.Key.ToLower(), out p))
                {
                    var propType = p.PropertyType;
                    if (kv.Value == null)
                    {
                        if (!propType.IsByRef && propType.Name != "Nullable`1")
                        {
                            // Throw if type is a value type 
                            // but not Nullable<>
                            throw new ArgumentException("not nullable");
                        }
                    }
                    else if (kv.Value.GetType() != propType)
                    {
                        // You could make this a bit less strict 
                        // but I don't recommend it.
                        throw new ArgumentException("type mismatch");
                    }
                    p.SetValue(destination, kv.Value, null);
                }
            }
            return destination;
        }
    }
}
