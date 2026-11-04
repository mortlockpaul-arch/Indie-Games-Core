using System;
using System.Runtime.InteropServices;

namespace A;

internal class c4827237edace17750ff18da99206638a
{
	internal const uint c67425a5958ff4bf25e0894b48d90001f = 32u;

	internal const int c3dcbce0ca771adcfe71c81b71b1be605 = 32768;

	internal const int c1c9f99381b3589fe5209431f733bc346 = 24576;

	internal const int c7bd38c84b9ff7009c33c5b7211ce9721 = 0;

	internal const int cd2d361d231e7c1fcbeac7e6c63b1fd47 = 2048;

	internal const int ccc0274759d8cbaeca706b2264f4a2a52 = 3;

	internal const int c86164948b648ff6d71d67441dd8c6026 = 4;

	internal const int c3696e32b455136798a96c930ecbde551 = 1;

	internal const int cf48ac38e549911f9f180d477eb75a7c1 = 32771;

	internal const int c8c389871f448a8464070f942b83225cd = 32772;

	internal const int cc0c6aee096eb43dd349f8ebfd350ea13 = 26625;

	internal static IntPtr c35a0821ae8621958406b5176450d4acc;

	static c4827237edace17750ff18da99206638a()
	{
		c35a0821ae8621958406b5176450d4acc = (IntPtr)0;
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSemaphore", SetLastError = true)]
	internal static extern IntPtr c8bffdb8e0fe096be9d7846540d8de1af(IntPtr c4cd87782bf8fc0a411f82f7ff63a9dd1, int cf3fb00c3bfb12254a4752685d05701b7, int c810258ebd4fe61c7af47ed6122e0666b, string ccfdd5487bfbd01ad9e15d54887d721b6);

	[DllImport("kernel32.dll", EntryPoint = "ReleaseSemaphore", SetLastError = true)]
	internal static extern bool c9ed61e89ab59b895303190f3a84a5ee6(IntPtr c647656327646507357f17bbe6e8eb310, int cffbd454b0e9fb9b25019caafedc3a9c7, out int cdaba69be69a106385fb34f7c9159b7f0);

	[DllImport("mscoree.dll", CharSet = CharSet.Unicode, EntryPoint = "StrongNameSignatureVerificationEx")]
	internal static extern bool c975dde062fe8b1bba25d8ee6418fb4a7(string c983ca4e00a45901eeab4110a359a22b1, bool cfba478880bc3d8abde75cff7de158e96, ref bool c8e5d5dd79ec646c81412e20ec5d453e2);
}
