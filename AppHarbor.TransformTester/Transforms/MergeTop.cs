using System.Linq;
using System.Xml;

namespace AppHarbor.TransformTester.Transforms
{
	public class MergeTop : Merge
	{
		protected override void InsertTransformElement(XmlElement targetElement, XmlElement transformElement)
		{
			targetElement.InsertBefore(transformElement, targetElement.ChildNodes.OfType<XmlNode>().FirstOrDefault());
		}
	}
}
