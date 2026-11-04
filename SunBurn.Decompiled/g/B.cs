using System;
using a;

namespace g
{
	internal class B
	{
		private a.B fNB;

		internal B(a.B P_0)
		{
			fNB = P_0;
		}

		internal void G(byte[] P_0, out string P_1, out uint P_2, out uint P_3, out DateTime P_4)
		{
			byte[] array = fNB.d(P_0);
			if (array.Length <= 8)
			{
				P_1 = "";
				P_2 = 0u;
				P_3 = 0u;
				P_4 = DateTime.Now;
				return;
			}
			P_1 = "";
			byte[] array2 = new byte[2];
			for (int i = 0; i < array.Length - 16; i++)
			{
				array2[0] = array[i];
				array2[1] = 0;
				P_1 += global::a.a.Q(array2, 0);
			}
			P_4 = DateTime.Now;
			P_2 = global::a.a._0011(array, array.Length - 8);
			P_3 = global::a.a._0011(array, array.Length - 4);
		}
	}
}
namespace G
{
	internal interface B
	{
		object Clone();
	}
}
