using System;

namespace NineRays.Obfuscator;

internal class Evaluation : Attribute
{
	internal readonly string Warning;

	internal Evaluation(string param_0)
	{
		Warning = param_0;
	}
}
