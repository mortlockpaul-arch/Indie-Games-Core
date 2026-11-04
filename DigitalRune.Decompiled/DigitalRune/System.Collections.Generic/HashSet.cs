using DigitalRune;

namespace System.Collections.Generic;

public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
{
	private readonly Dictionary<T, bool> c4c4682cb45d0f987d2b332b529479c3c;

	public int Count => c4c4682cb45d0f987d2b332b529479c3c.Count;

	private bool c02aea6f65acc1ac2c5e3545134255a92 => false;

	public HashSet()
	{
		c4c4682cb45d0f987d2b332b529479c3c = new Dictionary<T, bool>();
	}

	public bool Add(T element)
	{
		if (c4c4682cb45d0f987d2b332b529479c3c.ContainsKey(element))
		{
			return false;
		}
		c4c4682cb45d0f987d2b332b529479c3c.Add(element, value: true);
		return true;
	}

	private void c3f80c67a8a6bedd96b3e03d9b9d3aafb(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		Add(cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	void ICollection<T>.Add(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c3f80c67a8a6bedd96b3e03d9b9d3aafb
		this.c3f80c67a8a6bedd96b3e03d9b9d3aafb(cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	public void Clear()
	{
		c4c4682cb45d0f987d2b332b529479c3c.Clear();
	}

	public bool Contains(T element)
	{
		return c4c4682cb45d0f987d2b332b529479c3c.ContainsKey(element);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		c4c4682cb45d0f987d2b332b529479c3c.Keys.CopyTo(array, arrayIndex);
	}

	public bool Remove(T element)
	{
		return c4c4682cb45d0f987d2b332b529479c3c.Remove(element);
	}

	public int RemoveWhere(Predicate<T> match)
	{
		List<T> list = ResourcePools<T>.Lists.Obtain();
		using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator = c4c4682cb45d0f987d2b332b529479c3c.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (!match(current))
				{
					continue;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(HashSet<>.RemoveWhere(Predicate<T>));
				}
				list.Add(current);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_005b;
				}
				continue;
				end_IL_005b:
				break;
			}
		}
		int count = list.Count;
		foreach (T item in list)
		{
			Remove(item);
		}
		ResourcePools<T>.Lists.Recycle(list);
		return count;
	}

	public Dictionary<T, bool>.KeyCollection.Enumerator GetEnumerator()
	{
		return c4c4682cb45d0f987d2b332b529479c3c.Keys.GetEnumerator();
	}

	private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
	{
		return GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c0843ff4e059c4450bad00d5ba0b6ef48
		return this.c0843ff4e059c4450bad00d5ba0b6ef48();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}
}
