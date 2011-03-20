using System.Web.Mvc;

namespace AppHarbor.TransformTester.Controllers
{
	public class HomeController : Controller
	{
		[OutputCache(Duration=3600, VaryByParam="*")]
		public ActionResult Index()
		{
			ViewBag.DefaultWebConfig = Resources.DefaultWebConfig;
			ViewBag.DefaultTransform = Resources.DefaultTransformation;
			return View();
		}
	}
}
