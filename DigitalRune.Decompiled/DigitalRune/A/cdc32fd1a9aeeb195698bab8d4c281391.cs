using System;
using System.Reflection;

namespace A;

internal class cdc32fd1a9aeeb195698bab8d4c281391
{
	internal static void c77fb13d4e20e61c5b8c64ccfd3286846()
	{
		bool flag = false;
		try
		{
			string text = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10694);
			if (text != null && text.Length > 0)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(cdc32fd1a9aeeb195698bab8d4c281391.c77fb13d4e20e61c5b8c64ccfd3286846());
					}
					byte[] array = Convert.FromBase64String(text);
					if (array == null)
					{
						break;
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
						byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != publicKeyToken[i])
							{
								throw new Exception();
							}
						}
						flag = true;
						break;
					}
					break;
				}
			}
		}
		catch
		{
		}
		if (!flag)
		{
			string message = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10719) + Assembly.GetExecutingAssembly().GetName().Name + c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10802);
			throw new Exception(message);
		}
	}
}
