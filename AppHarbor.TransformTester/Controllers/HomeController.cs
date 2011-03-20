using System.Web.Mvc;

namespace AppHarbor.TransformTester.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.DefaultWebConfig = Resources.DefaultWebConfig;
			ViewBag.DefaultTransform = Resources.DefaultTransformation;
			return View();
		}
	}
}
