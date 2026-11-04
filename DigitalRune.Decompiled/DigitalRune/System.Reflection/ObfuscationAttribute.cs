namespace System.Reflection;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
public class ObfuscationAttribute : Attribute
{
	private bool _applyToMembers = true;

	private bool _exclude = true;

	private string _feature = "all";

	private bool _strip = true;

	public bool Exclude
	{
		get
		{
			return _exclude;
		}
		set
		{
			_exclude = value;
		}
	}

	public string Feature
	{
		get
		{
			return _feature;
		}
		set
		{
			_feature = value;
		}
	}

	public bool ApplyToMembers
	{
		get
		{
			return _applyToMembers;
		}
		set
		{
			_applyToMembers = value;
		}
	}

	public bool StripAfterObfuscation
	{
		get
		{
			return _strip;
		}
		set
		{
			_strip = value;
		}
	}
}
