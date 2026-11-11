using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorPropertyAttribute : Attribute
{
	private bool l8a;

	private string l8_u0013_;

	private string l8N;

	private int l8l;

	private int l8L;

	private bool l8w;

	private ControlType l8J;

	private bool l8B;

	internal bool EditorVisible => l8a;

	public string Description
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	public string ToolTipText
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public int MajorGrouping
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
			l8B = true;
		}
	}

	public int MinorGrouping
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	public bool HorizontalAlignment
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
		}
	}

	public ControlType ControlType
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	internal bool PositionSet => l8B;

	public EditorPropertyAttribute(bool editorvisible)
	{
		l8a = editorvisible;
		l8J = ControlType.Default;
	}
}
