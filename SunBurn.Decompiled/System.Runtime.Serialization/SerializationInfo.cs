using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using n;

namespace System.Runtime.Serialization;

public class SerializationInfo(Type unused1, FormatterConverter unused2) : IEnumerable
{
	private CultureInfo fNB = new CultureInfo("en-US");

	private Dictionary<string, SerializationEntry> fNa = new Dictionary<string, SerializationEntry>(16);

	public IEnumerator GetEnumerator()
	{
		return new n.B(fNa.GetEnumerator());
	}

	public void AddValue(string name, object value)
	{
		fNa.Add(name, new SerializationEntry(name, value));
	}

	public object GetValue(string name, Type datatype)
	{
		SerializationEntry serializationEntry = fNa[name];
		object value = serializationEntry.Value;
		if (!(value is string))
		{
			return value;
		}
		string text = (string)value;
		if ((object)datatype == typeof(string))
		{
			return text;
		}
		if ((object)datatype == typeof(int))
		{
			return int.Parse(text, fNB);
		}
		if ((object)datatype == typeof(float))
		{
			return float.Parse(text, fNB);
		}
		if ((object)datatype == typeof(bool))
		{
			return bool.Parse(text);
		}
		throw new Exception("Unknown data conversion.");
	}
}
