namespace WebApiProxy.Middleware
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Core.Infrastructure;
    using Core.Models;

    public class DefaultMetadataProvider : IMetadataProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionsProvider;
        public DefaultMetadataProvider(
            IApiDescriptionGroupCollectionProvider apiDescriptionsProvider)
        {
            _apiDescriptionsProvider = apiDescriptionsProvider;
        }

        public Metadata GetMetadata(string baseUrl = "")
        {
            var paths = _apiDescriptionsProvider.ApiDescriptionGroups.Items;

            var metadata = new Metadata
            {
                Host = baseUrl,
                Definitions =  paths.Select(_=> CreateControllerDefinition(_))
            };
            return metadata;
        }
        private ControllerDefinition CreateControllerDefinition(ApiDescriptionGroup apiDescriptions)
        {
            var controllerDefinition = new ControllerDefinition();

            // Group further by http method
            //var perMethodGrouping = apiDescriptions
            //    .GroupBy(apiDesc => apiDesc.GroupName);

            var actions = new List<ActionMethodDefinition>();

            foreach (var group in apiDescriptions.Items)
            {
                var httpMethod = group.HttpMethod;

                //if (httpMethod == null)
                //    throw new NotSupportedException(string.Format(
                //        "Unbounded HTTP verbs for path '{0}'. Are you missing an HttpMethodAttribute?",
                //        group.First()
                //        //.RelativePathSansQueryString()
                //        ));

                //if (group.Count() > 1)
                //    throw new NotSupportedException(string.Format(
                //        "Multiple operations with path '{0}' and method '{1}'. Are you overloading action methods?",
                //        group.First()
                //        //.RelativePathSansQueryString()
                //        , httpMethod));

                //var apiDescription = group.Single();

                actions.Add(CreateActionMethodDefinition(group
                    //, schemaRegistry
                    ));

                
            }
            controllerDefinition.Name = apiDescriptions.GroupName;
            controllerDefinition.ActionMethods = actions;
            return controllerDefinition;
        }

        private ActionMethodDefinition CreateActionMethodDefinition(ApiDescription apiDescription)
        {
            //var groupName = _options.GroupNameSelector(apiDescription);

            var parameters = apiDescription.ParameterDescriptions
                .Where(paramDesc => paramDesc.Source.IsFromRequest)
                .Select(paramDesc => CreateParameterDefinition(paramDesc))
                .ToList();

            //var responses = apiDescription.SupportedResponseTypes
            //    .DefaultIfEmpty(new ApiResponseType { StatusCode = 200 })
            //    .ToDictionary(
            //        apiResponseType => apiResponseType.StatusCode.ToString(),
            //        apiResponseType => CreateResponse(apiResponseType)
            //     );
            var action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)apiDescription.ActionDescriptor);
            var actionMethodDefinition = new ActionMethodDefinition
            {
                Name = action.ActionName,
                BodyParameter = parameters.SingleOrDefault(_=>_.Source == "body"),
                //Tags = (groupName != null) ? new[] { groupName } : null,
                Description = apiDescription.ActionDescriptor.DisplayName,
                //OperationId = apiDescription.FriendlyId(),
                //Consumes = apiDescription.SupportedRequestMediaTypes().ToList(),
                ReturnType = apiDescription.SupportedResponseTypes.FirstOrDefault()?.Type.ToString(),
                Url = apiDescription.RelativePath,
                UrlParameters = parameters.Any() ? parameters : new List<ParameterDefinition>(),
                Type = action.MethodInfo.DeclaringType.ToString()
                //    Produces = apiDescription.SupportedResponseMediaTypes().ToList(),
               // Parameters = parameters.Any() ? parameters : null, // parameters can be null but not empty
               // Responses = responses,
              //  Deprecated = apiDescription.IsObsolete()
            };

            //var filterContext = new OperationFilterContext(apiDescription, schemaRegistry);
            //foreach (var filter in _options.OperationFilters)
            //{
            //    filter.Apply(actionMethodDefinition, filterContext);
            //}

            return actionMethodDefinition;
        }

        private ParameterDefinition CreateParameterDefinition(ApiParameterDescription paramDesc
            //, ISchemaRegistry schemaRegistry
            )
        {
            var source = paramDesc.Source.Id.ToLower();
            //var schema = (paramDesc.Type == null) ? null : schemaRegistry.GetOrRegister(paramDesc.Type);

            
                return new ParameterDefinition
                {
                    Name = paramDesc.Name,
                    Source = source,
                    Type = paramDesc.Type.ToString(),
                    Description = paramDesc.ModelMetadata.Description
                    
                };
        }

        //private Response CreateResponse(ApiResponseType apiResponseType, ISchemaRegistry schemaRegistry)
        //{
        //    var description = ResponseDescriptionMap
        //        .FirstOrDefault((entry) => Regex.IsMatch(apiResponseType.StatusCode.ToString(), entry.Key))
        //        .Value;

        //    return new Response
        //    {
        //        Description = description,
        //        Schema = (apiResponseType.Type != null && apiResponseType.Type != typeof(void))
        //            ? schemaRegistry.GetOrRegister(apiResponseType.Type)
        //            : null
        //    };
        //}

        //private static readonly Dictionary<string, string> ResponseDescriptionMap = new Dictionary<string, string>
        //{
        //    { "1\\d{2}", "Information" },
        //    { "2\\d{2}", "Success" },
        //    { "3\\d{2}", "Redirect" },
        //    { "4\\d{2}", "Client Error" },
        //    { "5\\d{2}", "Server Error" }
        //};
    }
}
