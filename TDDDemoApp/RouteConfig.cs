using System.Web.Routing;

namespace TDDDemoApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "{*anything}", "~/index.html");
        }
    }
}