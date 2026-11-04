using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute for string properties to define textbox specific control options.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorTextBoxOptionsAttribute : BaseControlOptionsAttribute
{
	private int fNB;

	private bool fNa;

	/// <summary>
	/// Overrides the control width for this textbox.
	/// </summary>
	public int Width
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	internal bool ErrorTextBox => fNa;

	/// <summary>
	/// Creates a new EditorTextBoxOptionsAttribute instance.
	/// </summary>
	/// <param name="errortextbox">Defines whether or not this textbox will
	/// be an error textbox. Error textboxes are readonly and display their value
	/// in red text.</param>
	public EditorTextBoxOptionsAttribute(bool errortextbox)
	{
		fNa = errortextbox;
	}
}
