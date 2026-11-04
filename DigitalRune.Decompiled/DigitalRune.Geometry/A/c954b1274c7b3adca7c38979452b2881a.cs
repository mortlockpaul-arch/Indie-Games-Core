using System.IO;
using System.Reflection;
using System.Text;

namespace A;

internal class c954b1274c7b3adca7c38979452b2881a
{
	internal static byte[] c64f632f47ec42f89161439dfec6e213c;

	internal static string c9172a3c82181eb071d606cdbf4b22a9f(int c39291411c9363c8c148a1afc376604a8)
	{
		if (c64f632f47ec42f89161439dfec6e213c == null)
		{
			string name = "DigitalRune.GeometryDigitalRune.Geometry";
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
			c64f632f47ec42f89161439dfec6e213c = c1871d0fd169a3e51410d88ba41f4ff73.c8934277166c09ad334b08634b90c3092(manifestResourceStream);
		}
		int num = 0;
		if ((c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8] & 0x80) == 0)
		{
			num = c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8];
			c39291411c9363c8c148a1afc376604a8++;
		}
		else if ((c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8] & 0x40) == 0)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(int));
			}
			num = (c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8] & -129) << 8;
			num |= c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8 + 1];
			c39291411c9363c8c148a1afc376604a8 += 2;
		}
		else
		{
			num = (c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8] & -193) << 24;
			num |= c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8 + 1] << 16;
			num |= c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8 + 2] << 8;
			num |= c64f632f47ec42f89161439dfec6e213c[c39291411c9363c8c148a1afc376604a8 + 3];
			c39291411c9363c8c148a1afc376604a8 += 4;
		}
		if (num < 1)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					return string.Empty;
				}
			}
		}
		string str = Encoding.Unicode.GetString(c64f632f47ec42f89161439dfec6e213c, c39291411c9363c8c148a1afc376604a8, num);
		return string.Intern(str);
	}
}
