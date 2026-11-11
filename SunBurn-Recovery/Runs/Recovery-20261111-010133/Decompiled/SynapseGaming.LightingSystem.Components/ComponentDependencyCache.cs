using System;
using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Components;

public class ComponentDependencyCache
{
	private static Dictionary<Type, List<Type>> l8a = new Dictionary<Type, List<Type>>(16);

	public static List<Type> GetDependencies(Type componenttype)
	{
		if (l8a.TryGetValue(componenttype, out var value))
		{
			return value;
		}
		value = new List<Type>(4);
		value.Add(componenttype);
		_u0012_(componenttype, value);
		l8a.Add(componenttype, value);
		return value;
	}

	private static void _u0012_(Type param_0, List<Type> param_1)
	{
		object[] customAttributes = param_0.GetCustomAttributes(typeof(ComponentDependencyAttribute), inherit: true);
		object[] array = customAttributes;
		foreach (object obj in array)
		{
			if (obj is ComponentDependencyAttribute componentDependencyAttribute && !param_1.Contains(componentDependencyAttribute.Dependency))
			{
				param_1.Add(componentDependencyAttribute.Dependency);
				_u0012_(componentDependencyAttribute.Dependency, param_1);
			}
		}
	}
}
