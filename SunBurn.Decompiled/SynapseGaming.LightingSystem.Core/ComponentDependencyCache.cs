using System;
using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Used to find and cache a component's dependencies.
/// </summary>
public class ComponentDependencyCache
{
	private static Dictionary<Type, List<Type>> fNB = new Dictionary<Type, List<Type>>(16);

	/// <summary>
	/// Find and cache the component's dependencies.
	/// </summary>
	/// <param name="componenttype"></param>
	/// <returns>List of types the component is dependent on.
	///
	/// WARNING: the list is a shared reference. Do not alter the list or it will
	/// affect internal component dependencies.</returns>
	public static List<Type> GetDependencies(Type componenttype)
	{
		if (fNB.TryGetValue(componenttype, out var value))
		{
			return value;
		}
		value = new List<Type>(4);
		value.Add(componenttype);
		E(componenttype, value);
		fNB.Add(componenttype, value);
		return value;
	}

	private static void E(Type P_0, List<Type> P_1)
	{
		object[] customAttributes = P_0.GetCustomAttributes(typeof(ComponentDependencyAttribute), inherit: true);
		object[] array = customAttributes;
		foreach (object obj in array)
		{
			if (obj is ComponentDependencyAttribute componentDependencyAttribute && !P_1.Contains(componentDependencyAttribute.Dependency))
			{
				P_1.Add(componentDependencyAttribute.Dependency);
				E(componentDependencyAttribute.Dependency, P_1);
			}
		}
	}
}
