using System;
using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Core;

public class TypeDictionary<T>
{
	private Dictionary<Type, T> l8a = new Dictionary<Type, T>();

	public Dictionary<Type, T> Items => l8a;

	public void Add(Type type, T item)
	{
		l8a[type] = item;
	}

	public void Remove(Type type)
	{
		l8a.Remove(type);
	}

	public T GetItem(Type type)
	{
		if (l8a.TryGetValue(type, out var value))
		{
			return value;
		}
		return default;
	}
}
