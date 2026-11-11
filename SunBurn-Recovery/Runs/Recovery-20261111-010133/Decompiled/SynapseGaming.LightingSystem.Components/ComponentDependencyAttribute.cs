using System;
using System.Runtime.CompilerServices;

namespace SynapseGaming.LightingSystem.Components;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ComponentDependencyAttribute : Attribute
{
	[CompilerGenerated]
	private Type l8a;

	public Type Dependency
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		set
		{
			l8a = value;
		}
	}

	public ComponentDependencyAttribute(Type dependency)
	{
		Dependency = dependency;
	}
}
