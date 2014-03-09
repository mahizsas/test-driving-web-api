using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NUnit.Framework;
using Wep_API_route_tests;

namespace UnitTests
{
    
    public class RouteTests
    {
        [Test]
        public void GetCustomerIsHandledByCustomerController()
        {
            var httpConfiguration = new HttpConfiguration(new HttpRouteCollection());
            WebApiConfig.Register(httpConfiguration);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://dummylocalhost/api/Customer?id=1001");
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = httpConfiguration.Routes.GetRouteData(request);

            var controllerBuilder = new TestControllerBuilder(request, httpConfiguration);

            Assert.That(controllerBuilder.GetControllerName(), Is.EqualTo("CustomerController"));
            Assert.That(controllerBuilder.GetActionName(), Is.EqualTo("Get"));
        }

        [Test]
        public void GetCustomerIsHandledByCustomerController2()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://dummylocalhost/api/Customer?id=1001");

            Assert.That(request, IsHandledBy.Controller("CustomerController"));
            Assert.That(request, IsHandledBy.Action("Get"));
        }
    }
}
