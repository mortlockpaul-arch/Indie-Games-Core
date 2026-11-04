using System;

namespace v;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
internal class B : Attribute
{
	private string fNB;

	private bool fNa;

	internal string TexturePathProperty => fNB;

	internal bool TextureRequiresFloatFormat => fNa;

	public B(string texturepathproperty, bool texturerequiresfloatformat)
	{
		fNB = texturepathproperty;
		fNa = texturerequiresfloatformat;
	}
}
