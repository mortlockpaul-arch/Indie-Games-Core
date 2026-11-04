using System;
using System.IO;
using System.Xml.Linq;

namespace _0019
{
	internal class n
	{
		public _0003 DocumentElement;

		public void Load(Stream stream)
		{
			XDocument xDocument = XDocument.Load(stream);
			DocumentElement = new _0003();
			DocumentElement.Load(xDocument.Root);
		}
	}
}
namespace _0011
{
	internal class n : _0003
	{
		public n()
			: base("Unexpected error occured during a cryptographic operation.")
		{
			HResult = -2146233295;
		}

		public n(string message)
			: base(message)
		{
			HResult = -2146233295;
		}

		public n(string message, Exception inner)
			: base(message, inner)
		{
			HResult = -2146233295;
		}

		public n(string format, string insert)
			: base(string.Format(format, insert))
		{
			HResult = -2146233295;
		}
	}
}
