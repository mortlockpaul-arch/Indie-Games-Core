using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorTextBoxOptionsAttribute : BaseControlOptionsAttribute
{
	private int l8a;

	private bool l8_u0013_;

	public int Width
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	internal bool ErrorTextBox => l8_u0013_;

	public EditorTextBoxOptionsAttribute(bool errortextbox)
	{
		l8_u0013_ = errortextbox;
	}
}
