using System;
using System.Diagnostics;

namespace B;

internal class B
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static uint fNB;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static uint fNa = 1000000u;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool fNg = false;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static f fNf;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	internal static string fN_0003 = "";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool fNn = false;

	[DebuggerHidden]
	private static void InitializeState()
	{
		if (fNf == null)
		{
			fNB = global::B.a.n();
			fNf = new f();
		}
	}

	[DebuggerHidden]
	internal static void a(byte[] P_0)
	{
		InitializeState();
		if (!fNf._8(P_0))
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static void g()
	{
		InitializeState();
		if (!fNn)
		{
			fNn = true;
			fNg = false;
			fNg = fNf._0005(global::B.g.fNa[1].FileName, global::B.g.fNa[1].DRMProductName, fNB);
			fNa = 0u;
		}
		fNa++;
		if (!fNg)
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static void f()
	{
		InitializeState();
		if (fNa > 1000)
		{
			fNg = false;
			fNa = 0u;
			fNg = fNf._0005(global::B.g.fNa[1].FileName, global::B.g.fNa[1].DRMProductName, fNB);
		}
		fNa++;
		if (!fNg)
		{
			throw new Exception("Product not activated, please run activation tool.");
		}
	}

	[DebuggerHidden]
	internal static string _0003()
	{
		return global::B.g.ActivationPath + global::B.g.fNa[1].FileName;
	}
}

