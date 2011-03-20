using System.Web.Mvc;
using System.Web.Routing;

namespace AppHarbor.TransformTester
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Home",
				"",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				"Transform",
				"transform",
				new { controller = "Transform", action = "Create", id = UrlParameter.Optional }
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}