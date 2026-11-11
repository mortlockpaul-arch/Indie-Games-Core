using System;

namespace B;

internal class _u0002_<T> : global::B.h<T> where T : IDisposable, new()
{
	public override void Clear()
	{
		FreeAllTracked();
		foreach (T item in _UnusedObjectPool)
		{
			item.Dispose();
		}
		_UnusedObjectPool.Clear();
		if (_LostObjectCount > 0)
		{
			throw new Exception("Some tracked pool objects were not disposed.");
		}
	}

	public void Unload()
	{
		Clear();
	}
}
