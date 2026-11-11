using System.Collections.Generic;

namespace B;

internal class Y
{
	private struct w8a
	{
		internal string l8a;

		internal float l8_u0013_;

		internal w8a(string param_0, float param_1)
		{
			l8a = param_0;
			l8_u0013_ = param_1;
		}
	}

	private static List<w8a> l8a = new List<w8a>(32);

	internal static void _u0013_V(string param_0)
	{
		l8a.Add(new w8a(param_0, -1f));
	}

	internal static void _u0013__u0011_(string param_0, float param_1)
	{
		l8a.Add(new w8a(param_0, param_1));
	}

	internal static void _u0013__u0003_(string param_0)
	{
	}
}
