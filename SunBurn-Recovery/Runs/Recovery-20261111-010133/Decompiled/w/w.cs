using System.IO;
using System.Xml.Linq;

namespace w;

internal class w
{
	public L DocumentElement;

	public void Load(Stream stream)
	{
		XDocument xDocument = XDocument.Load(stream);
		DocumentElement = new L();
		DocumentElement.Load(xDocument.Root);
	}
}
