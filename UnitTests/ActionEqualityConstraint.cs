using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NUnit.Framework.Constraints;
using Wep_API_route_tests;

namespace UnitTests
{
    public class ActionEqualityConstraint : Constraint
    {
        private readonly string action;

        public ActionEqualityConstraint(string action)
        {
            this.action = action;
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
                return action == controllerBuilder.GetActionName();
            }

            return false;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write(action);
        }
    }
}