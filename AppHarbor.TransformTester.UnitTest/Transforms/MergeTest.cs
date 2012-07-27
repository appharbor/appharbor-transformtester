using System.Collections.Generic;
using System.IO;
using System.Xml;
using AppHarbor.TransformTester.Transforms;
using Xunit;
using Xunit.Extensions;

namespace AppHarbor.TransformTester.UnitTest.Transforms
{
	public class MergeTest
	{
		[Theory]
		[InlineData("<foo></foo>", "<bar />", "<foo><bar /></foo>")]
		[InlineData("<foo></foo>", "<bar></bar>", "<foo><bar></bar></foo>")]
		[InlineData("<foo><bar></bar></foo>", "<bar></bar>", "<foo><bar></bar></foo>")]
		[InlineData("<foo><baz></baz></foo>", "<bar></bar>", "<foo><baz></baz><bar></bar></foo>")]
		public void Apply_ShouldMerge(string targetXml, string transformXml, string expectedXml)
		{
			var targetDocument = ArrangeTargetDocument(targetXml);
			var transformElement = ArrangeTransformElement(targetDocument, transformXml);

			var merge = new Merge();
			merge.Apply(targetDocument.DocumentElement, transformElement);

			Assert.Equal(expectedXml, targetDocument.OuterXml);
		}

		[Theory]
		[InlineData("<foo><baz></baz></foo>", "<bar></bar>", "<foo><bar></bar><baz></baz></foo>", new[] { "/foo/baz" })]
		[InlineData("<foo><baz></baz></foo>", "<bar></bar>", "<foo><bar></bar><baz></baz></foo>", new[] { "/foo/*" })]
		[InlineData("<foo><baz></baz></foo>", "<baz></baz>", "<foo><baz></baz></foo>", new[] { "/foo/*" })]
		[InlineData("<foo><bar><baz></baz></bar></foo>", "<baz></baz>", "<foo><bar><baz></baz></bar></foo>", new[] { "/foo/bar/*" })]
		public void Apply_ShouldMergeTop(string targetXml, string transformXml, string expectedXml, IList<string> arguments)
		{
			var targetDocument = ArrangeTargetDocument(targetXml);
			var transformElement = ArrangeTransformElement(targetDocument, transformXml);

			var merge = new MergeBefore();
			merge.Apply(targetDocument.DocumentElement, transformElement, arguments);

			Assert.Equal(expectedXml, targetDocument.OuterXml);
		}

		private XmlDocument ArrangeTargetDocument(string xml)
		{
			var document = new XmlDocument();
			document.LoadXml(xml);

			return document;
		}

		private XmlElement ArrangeTransformElement(XmlDocument document, string xml)
		{
			using (var textReader = new StringReader(xml))
			{
				using (var xmlReader = new XmlTextReader(textReader))
				{
					var node = document.ReadNode(xmlReader);
					return (XmlElement)node;
				}
			}
		}
	}
}
