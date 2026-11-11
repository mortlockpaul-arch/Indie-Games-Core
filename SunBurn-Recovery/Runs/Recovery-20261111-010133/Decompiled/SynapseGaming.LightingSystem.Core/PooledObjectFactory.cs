using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Core;

public class PooledObjectFactory<T> where T : new()
{
	protected List<T> _UnusedObjectPool = new List<T>();

	protected int _LostObjectCount;

	public virtual T New()
	{
		_LostObjectCount++;
		if (_UnusedObjectPool.Count < 1)
		{
			return new T();
		}
		int index = _UnusedObjectPool.Count - 1;
		T result = _UnusedObjectPool[index];
		_UnusedObjectPool.RemoveAt(index);
		return result;
	}

	public virtual void Free(T obj)
	{
		_LostObjectCount--;
		_UnusedObjectPool.Add(obj);
	}

	public virtual void Clear()
	{
		_UnusedObjectPool.Clear();
	}

	public virtual void Clear(List<T> returnedobjects)
	{
		foreach (T item in _UnusedObjectPool)
		{
			returnedobjects.Add(item);
		}
		_UnusedObjectPool.Clear();
	}
}
