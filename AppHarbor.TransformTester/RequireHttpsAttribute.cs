using System;
using System.Web.Mvc;
using RequireHttpsAttributeBase = System.Web.Mvc.RequireHttpsAttribute;

namespace AppHarbor.TransformTester
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class RequireHttpsAttribute : RequireHttpsAttributeBase
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}

			if (filterContext.HttpContext.Request.IsSecureConnection)
			{
				return;
			}

			if (string.Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"],
				"https",
				StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			if (filterContext.HttpContext.Request.IsLocal)
			{
				return;
			}

			HandleNonHttpsRequest(filterContext);
		}
	}
}
