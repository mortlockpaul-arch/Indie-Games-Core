using System;
using System.Diagnostics;

namespace a;

internal class a
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static uint l8a;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool l8_u0013_ = false;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static l l8N;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	internal static string l8l = "";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool l8L = false;

	[DebuggerHidden]
	private static void a_method()
	{
		if (l8N == null)
		{
			l8a = global::a._u0013_.w();
			l8N = new l();
		}
	}

	[DebuggerHidden]
	internal static void _u0013_(byte[] param_0)
	{
		a_method();
		if (!l8N.v(param_0))
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static void N()
	{
		a_method();
		if (!l8L)
		{
			l8L = true;
			l8_u0013_ = false;
			l8_u0013_ = l8N._u0005_(global::a.N.l8_u0013_[1].FileName, global::a.N.l8_u0013_[1].DRMProductName, l8a);
		}
		if (!l8_u0013_)
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static void l()
	{
		a_method();
		if (!l8L)
		{
			l8L = true;
			l8_u0013_ = false;
			l8_u0013_ = l8N._u0005_(global::a.N.l8_u0013_[1].FileName, global::a.N.l8_u0013_[1].DRMProductName, l8a);
		}
		if (!l8_u0013_)
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static string L()
	{
		return global::a.N.ActivationPath + global::a.N.l8_u0013_[1].FileName;
	}
}
