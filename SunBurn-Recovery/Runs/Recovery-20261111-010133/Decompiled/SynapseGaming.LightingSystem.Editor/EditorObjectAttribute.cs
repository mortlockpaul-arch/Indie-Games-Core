using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
public class EditorObjectAttribute : Attribute
{
	private bool l8a;

	internal bool OnlyMarkedProperties => l8a;

	public EditorObjectAttribute(bool onlymarkedproperties)
	{
		l8a = onlymarkedproperties;
	}
}
