using B;
using Microsoft.Xna.Framework.Content;

namespace m;

internal class B
{
	internal static void _0003F(ContentReader P_0)
	{
		byte[] array = null;
		try
		{
			int count = P_0.ReadInt32();
			array = P_0.ReadBytes(count);
		}
		catch
		{
		}
		global::B.B.a(array);
	}
}
