using System.Web;
using System.Web.Mvc;

namespace Wep_API_route_tests
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}