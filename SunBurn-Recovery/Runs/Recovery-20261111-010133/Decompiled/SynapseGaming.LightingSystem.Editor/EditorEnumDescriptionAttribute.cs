using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class EditorEnumDescriptionAttribute : Attribute
{
	private string l8a;

	private bool l8_u0013_;

	internal string Description => l8a;

	internal bool Ignore => l8_u0013_;

	public EditorEnumDescriptionAttribute(string description)
	{
		l8a = description;
		l8_u0013_ = false;
	}

	public EditorEnumDescriptionAttribute(bool ignore)
	{
		l8_u0013_ = ignore;
	}
}
