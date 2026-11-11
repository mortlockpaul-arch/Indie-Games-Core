using System;

namespace NineRays.Obfuscator;

internal class SoftwareWatermarkAttribute : Attribute
{
	internal readonly string Watermark;

	internal SoftwareWatermarkAttribute(string param_0)
	{
		Watermark = param_0;
	}
}
