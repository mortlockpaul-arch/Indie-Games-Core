using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Core;

public class ObjectIndex<T> where T : IMovableObject
{
	private Dictionary<T, int> l8a = new Dictionary<T, int>(32);

	private Dictionary<T, int> l8_u0013_ = new Dictionary<T, int>(32);

	private Dictionary<string, T> l8N = new Dictionary<string, T>(32);

	private Dictionary<string, T> l8l = new Dictionary<string, T>(32);

	private Dictionary<int, T> l8L = new Dictionary<int, T>(32);

	public Dictionary<T, int> DynamicObjects => l8a;

	public Dictionary<T, int> AllObjects => l8_u0013_;

	public Dictionary<string, T> DynamicObjectsByName => l8N;

	public Dictionary<string, T> AllObjectsByName => l8l;

	public Dictionary<int, T> AllObjectsByUniqueId => l8L;

	public void Add(T obj)
	{
		string text = obj.Name;
		if (text == null)
		{
			text = string.Empty;
		}
		l8_u0013_[obj] = obj.MoveId;
		l8l[text] = obj;
		l8L[obj.UniqueId] = obj;
		if (obj.UpdateType == UpdateType.Automatic)
		{
			l8a[obj] = obj.MoveId;
			l8N[text] = obj;
		}
	}

	public void Remove(T obj)
	{
		string text = obj.Name;
		if (text == null)
		{
			text = string.Empty;
		}
		l8_u0013_.Remove(obj);
		l8a.Remove(obj);
		l8l.Remove(text);
		l8N.Remove(text);
		l8L.Remove(obj.UniqueId);
	}

	public void Clear()
	{
		l8_u0013_.Clear();
		l8a.Clear();
		l8l.Clear();
		l8N.Clear();
		l8L.Clear();
	}
}
