using Microsoft.Xna.Framework.Content;
using a;

namespace _id_6;

internal class a
{
	internal static void L_u0005_(ContentReader param_0)
	{
		byte[] param_1 = null;
		try
		{
			int count = param_0.ReadInt32();
			param_1 = param_0.ReadBytes(count);
		}
		catch
		{
		}
		global::a.a._u0013_(param_1);
	}
}
