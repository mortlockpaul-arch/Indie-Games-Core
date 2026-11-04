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
			string text = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18269);
			if (text != null && text.Length > 0)
			{
				byte[] array = Convert.FromBase64String(text);
				if (array != null)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						if (1 == 0)
						{
							__ldtoken(cdc32fd1a9aeeb195698bab8d4c281391.c77fb13d4e20e61c5b8c64ccfd3286846());
						}
						AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
						byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] == publicKeyToken[i])
							{
								continue;
							}
							while (true)
							{
								switch (4)
								{
								case 0:
									continue;
								}
								throw new Exception();
							}
						}
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							flag = true;
							break;
						}
						break;
					}
				}
			}
		}
		catch
		{
		}
		if (!flag)
		{
			string message = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18294) + Assembly.GetExecutingAssembly().GetName().Name + c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18377);
			throw new Exception(message);
		}
	}
}
