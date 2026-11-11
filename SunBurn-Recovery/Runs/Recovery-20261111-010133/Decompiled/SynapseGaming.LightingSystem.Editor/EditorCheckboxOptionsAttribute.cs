using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorCheckboxOptionsAttribute : BaseControlOptionsAttribute
{
	private bool l8a;

	internal bool IntegerValue => l8a;

	public EditorCheckboxOptionsAttribute(bool integervalue)
	{
		l8a = integervalue;
	}
}
