using System.Collections.Generic;

namespace w;

internal class l : List<N>
{
	public a GetNamedItem(string name)
	{
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				a current = enumerator.Current;
				if (current.Name == name)
				{
					return current;
				}
			}
		}
		return null;
	}
}
internal class L : a
{
}
