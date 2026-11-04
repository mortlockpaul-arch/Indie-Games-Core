namespace System.Runtime.Serialization;

public class SerializationEntry
{
	private string fNB = "";

	private object fNa;

	public string Name => fNB;

	public object Value => fNa;

	public SerializationEntry(string name, object value)
	{
		fNB = name;
		fNa = value;
	}
}
