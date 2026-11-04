using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;

namespace L;

internal class i<T> : PooledObjectFactory<T> where T : new()
{
	private List<T> fNB = new List<T>();

	public override T New()
	{
		T val = base.New();
		fNB.Add(val);
		return val;
	}

	public override void Free(T obj)
	{
		fNB.Remove(obj);
		base.Free(obj);
	}

	public void FreeAllTracked()
	{
		foreach (T item in fNB)
		{
			base.Free(item);
		}
		fNB.Clear();
	}

	public override void Clear()
	{
		FreeAllTracked();
		base.Clear();
	}
}
