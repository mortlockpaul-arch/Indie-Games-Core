using System.Collections.Generic;
using B;

namespace SynapseGaming.LightingSystem.Core;

public class TypeCasters<TTypeCaster, TType> where TTypeCaster : ITypeCaster<TType>, new()
{
	private Dictionary<TType, TTypeCaster> l8a = new Dictionary<TType, TTypeCaster>(128);

	private B.h<TTypeCaster> l8_u0013_ = new B.h<TTypeCaster>();

	public void Clear()
	{
		l8a.Clear();
		l8_u0013_.FreeAllTracked();
	}

	public TTypeCaster Get(TType obj)
	{
		if (l8a.TryGetValue(obj, out var value))
		{
			return value;
		}
		value = l8_u0013_.New();
		value.Set(obj);
		l8a.Add(obj, value);
		return value;
	}
}
