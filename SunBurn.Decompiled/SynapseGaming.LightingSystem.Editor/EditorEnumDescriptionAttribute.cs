using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute to give enum values human-readable descriptions to be displayed in-editor.
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class EditorEnumDescriptionAttribute : Attribute
{
	private string fNB;

	private bool fNa;

	internal string Description => fNB;

	internal bool Ignore => fNa;

	/// <summary>
	/// Creates a new EditorEnumDescriptionAttribute instance.
	/// </summary>
	/// <param name="description">The human-readable description to display
	/// in-editor.</param>
	public EditorEnumDescriptionAttribute(string description)
	{
		fNB = description;
		fNa = false;
	}

	/// <summary>
	/// Creates a new EditorEnumDescriptionAttribute instance.
	/// </summary>
	/// <param name="ignore">Defines whether or not this enum value will
	/// be hidden in-editor.</param>
	public EditorEnumDescriptionAttribute(bool ignore)
	{
		fNa = ignore;
	}
}
