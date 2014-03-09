using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace UnitTests
{
    public class TestControllerBuilder
    {
        private readonly ApiControllerActionSelector actionSelector;
        private readonly HttpControllerContext controllerContext;
        private readonly HttpControllerDescriptor controlleDescriptor;
        private readonly HttpRequestMessage requestMessage;
        private readonly HttpConfiguration httpConfiguration;

        public TestControllerBuilder(HttpRequestMessage request, HttpConfiguration httpConfiguration)
        {
            var routeData = request.Properties[HttpPropertyKeys.HttpRouteDataKey] as IHttpRouteData;
            controllerContext = new HttpControllerContext(httpConfiguration, routeData, request);
            IHttpControllerSelector controllerSelector = httpConfiguration.Services.GetHttpControllerSelector();
            controlleDescriptor = controllerSelector.SelectController(request);
            controllerContext.ControllerDescriptor = controlleDescriptor;
            actionSelector = new ApiControllerActionSelector();
            this.httpConfiguration = httpConfiguration;
            requestMessage = request;
        }

        public string GetActionName()
        {
            var actionDescriptor = actionSelector.SelectAction(controllerContext);
            return actionDescriptor.ActionName;
        }

        public string GetControllerName()
        {
            var controllerType = controlleDescriptor.ControllerType;
            return controllerType.Name;
        }

        public HttpControllerContext HttpControllerContext
        {
            get { return controllerContext; }
        }

        public T Build<T>() where T : ApiController, new()
        {
            var apiController = new T();
            Setup(apiController);
            return apiController;
        }

        public void Setup<T>(T apiController) where T : ApiController, new()
        {
            apiController.ControllerContext = HttpControllerContext;
            apiController.Request = requestMessage;

            apiController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }
    }
}