using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute for enum properties to define dropdownbox specific control options.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorDropDownOptionsAttribute : BaseControlOptionsAttribute
{
	private int fNB;

	private string[] fNa;

	internal int Width => fNB;

	internal string[] Values => fNa;

	/// <summary>
	/// Creates a new EditorDropDownOptionsAttribute instance.
	/// </summary>
	/// <param name="width">Overrides the control width for this dropdownbox.</param>
	public EditorDropDownOptionsAttribute(int width)
	{
		fNB = width;
	}

	internal EditorDropDownOptionsAttribute(string[] P_0, int P_1)
	{
		fNa = P_0;
		fNB = P_1;
	}
}
