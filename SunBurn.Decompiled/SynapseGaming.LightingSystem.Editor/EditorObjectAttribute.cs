using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute that marks the class as editable inside the SunBurn Editor. 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
public class EditorObjectAttribute : Attribute
{
	private bool fNB;

	internal bool OnlyMarkedProperties => fNB;

	/// <summary>
	/// Creates a new EditorObjectAttribute instance.
	/// </summary>
	/// <param name="onlymarkedproperties">Defines whether or not the editor will ignore
	/// public properties that do not have an EditorPropertyAttribute.</param>
	public EditorObjectAttribute(bool onlymarkedproperties)
	{
		fNB = onlymarkedproperties;
	}
}
