using System;
using System.Runtime.CompilerServices;

namespace SynapseGaming.LightingSystem.Core;

[AttributeUsage(AttributeTargets.Assembly)]
public class SunBurnPluginTypeAttribute : Attribute
{
	[CompilerGenerated]
	private string l8a;

	[CompilerGenerated]
	private string l8_u0013_;

	public string AssemblyQualifiedName
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		private set
		{
			l8a = value;
		}
	}

	public string FullName
	{
		[CompilerGenerated]
		get
		{
			return l8_u0013_;
		}
		[CompilerGenerated]
		private set
		{
			l8_u0013_ = value;
		}
	}

	public SunBurnPluginTypeAttribute(Type type)
	{
		AssemblyQualifiedName = type.AssemblyQualifiedName;
		FullName = type.FullName;
	}
}
