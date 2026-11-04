using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute that marks properties as editable inside the SunBurn Editor and 
/// defines UI-behavior.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorPropertyAttribute : Attribute
{
	private bool fNB;

	private string fNa;

	private string fNg;

	private int fNf;

	private int fN_0003;

	private bool fNn;

	private ControlType fN_0019;

	private bool fNL;

	internal bool EditorVisible => fNB;

	/// <summary>
	/// The human-readable description for this property to be displayed in-editor.
	/// </summary>
	public string Description
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// The Tooltip information to display when the mouse hovers over the
	/// control for this property.
	/// </summary>
	public string ToolTipText
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// The major grouping for organizing properties. Major groups are separated
	/// by dividers in the properties panel.
	/// </summary>
	public int MajorGrouping
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = value;
			fNL = true;
		}
	}

	/// <summary>
	/// The minor grouping for organizing properties. This defines the order of 
	/// properties within the same major group.
	/// </summary>
	public int MinorGrouping
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// Defines the position of the description label for this property. True for the label
	/// to appear next to the control, false for it to appear above the control.
	/// </summary>
	public bool HorizontalAlignment
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Overrides the default control type. Useful to display Vector3 properties
	/// as a color selection box.
	/// Use with caution, will create unexpected results for mismatched datatypes.
	/// </summary>
	public ControlType ControlType
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	internal bool PositionSet => fNL;

	/// <summary>
	/// Creates a new EditorPropertyAttribute instance.
	/// </summary>
	/// <param name="editorvisible">Defines whether or not this property will be
	/// displayed in-editor.</param>
	public EditorPropertyAttribute(bool editorvisible)
	{
		fNB = editorvisible;
		fN_0019 = ControlType.Default;
	}
}
