using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorDropDownOptionsAttribute : BaseControlOptionsAttribute
{
	private int l8a;

	private string[] l8_u0013_;

	internal int Width => l8a;

	internal string[] Values => l8_u0013_;

	public EditorDropDownOptionsAttribute(int width)
	{
		l8a = width;
	}

	internal EditorDropDownOptionsAttribute(string[] param_0, int param_1)
	{
		l8_u0013_ = param_0;
		l8a = param_1;
	}
}
