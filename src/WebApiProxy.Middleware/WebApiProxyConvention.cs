namespace WebApiProxy.Middleware
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    public class WebApiProxyConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
            foreach (var controller in application.Controllers)
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }
        }
    }
}
