using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace n;

internal class B : IEnumerator
{
	private IEnumerator fNB;

	public object Current => ((KeyValuePair<string, SerializationEntry>)fNB.Current).Value;

	public B(IEnumerator items)
	{
		fNB = items;
	}

	public bool MoveNext()
	{
		return fNB.MoveNext();
	}

	public void Reset()
	{
		fNB.Reset();
	}
}
