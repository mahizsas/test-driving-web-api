using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NUnit.Framework.Constraints;
using Wep_API_route_tests;

namespace UnitTests
{
    internal class ControllerEqualityConstraint : Constraint
    {
        private readonly string controller;

        public ControllerEqualityConstraint(string controller)
        {
            this.controller = controller;
        }

        public override bool Matches(object item)
        {
            var request = (HttpRequestMessage)item;

            if (request != null)
            {
                var httpConfiguration = new HttpConfiguration(new HttpRouteCollection());
                WebApiConfig.Register(httpConfiguration);
                request.Properties[HttpPropertyKeys.HttpRouteDataKey] = httpConfiguration.Routes.GetRouteData(request);

                var controllerBuilder = new TestControllerBuilder(request, httpConfiguration);
                return controller == controllerBuilder.GetControllerName();
            }

            return false;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write(controller);
        }
    }
}