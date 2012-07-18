using System.Linq;
using System.Xml;
using Microsoft.Web.Publishing.Tasks;

namespace AppHarbor.TransformTester.Transforms
{
	public class Merge : Transform
	{
		public Merge()
			: base(TransformFlags.UseParentAsTargetNode)
		{
		}

		protected override void Apply()
		{
			Apply((XmlElement)TargetNode, (XmlElement)TransformNode);
		}

		public static void Apply(XmlElement targetElement, XmlElement transformElement)
		{
			var targetChildElement = targetElement.ChildNodes.OfType<XmlElement>().FirstOrDefault(x => x.LocalName == transformElement.LocalName);
			if (targetChildElement == null)
			{
				targetElement.AppendChild(transformElement);
				return;
			}

			foreach (var transformChildElement in transformElement.ChildNodes.OfType<XmlElement>())
			{
				Apply(targetChildElement, transformChildElement);
			}
		}

	}
}
