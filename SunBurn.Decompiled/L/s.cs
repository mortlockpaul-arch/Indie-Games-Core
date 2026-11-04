using System.Collections.Generic;

namespace L;

internal class s<TTypeCaster, TType> where TTypeCaster : global::L.j<TType>, new()
{
	private Dictionary<TType, TTypeCaster> fNB = new Dictionary<TType, TTypeCaster>(128);

	private global::L.i<TTypeCaster> fNa = new global::L.i<TTypeCaster>();

	internal void _0013()
	{
		fNB.Clear();
		fNa.FreeAllTracked();
	}

	internal TTypeCaster g_0011(TType P_0)
	{
		if (fNB.TryGetValue(P_0, out var value))
		{
			return value;
		}
		value = fNa.New();
		value.Set(P_0);
		fNB.Add(P_0, value);
		return value;
	}
}
