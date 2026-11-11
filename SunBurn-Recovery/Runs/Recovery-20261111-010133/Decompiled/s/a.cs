using System;

namespace s;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
internal class a : Attribute
{
	private string l8a;

	private bool l8_u0013_;

	internal string TexturePathProperty => l8a;

	internal bool TextureRequiresFloatFormat => l8_u0013_;

	public a(string texturepathproperty, bool texturerequiresfloatformat)
	{
		l8a = texturepathproperty;
		l8_u0013_ = texturerequiresfloatformat;
	}
}
