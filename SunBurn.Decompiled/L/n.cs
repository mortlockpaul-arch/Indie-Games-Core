using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace L;

internal class n
{
	private static Dictionary<Type, Dictionary<string, PropertyInfo>> fNB = new Dictionary<Type, Dictionary<string, PropertyInfo>>(128);

	private static Dictionary<string, PropertyInfo> aj(Type P_0)
	{
		if (fNB.ContainsKey(P_0))
		{
			return fNB[P_0];
		}
		PropertyInfo[] properties = P_0.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(properties.Length);
		fNB.Add(P_0, dictionary);
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			if (!(propertyInfo.Name == "UniqueId") && !(propertyInfo.Name == "CollisionMove") && ((object)propertyInfo.DeclaringType != typeof(Effect) || !(propertyInfo.Name == "CurrentTechnique")))
			{
				dictionary.Add(propertyInfo.Name, propertyInfo);
			}
		}
		return dictionary;
	}

	internal static void ao(object P_0, object P_1)
	{
		Type type = P_0.GetType();
		Type type2 = P_1.GetType();
		Dictionary<string, PropertyInfo> dictionary = aj(type);
		Dictionary<string, PropertyInfo> dictionary2 = aj(type2);
		foreach (string key in dictionary.Keys)
		{
			if (dictionary.ContainsKey(key) && dictionary2.ContainsKey(key))
			{
				PropertyInfo propertyInfo = dictionary[key];
				PropertyInfo propertyInfo2 = dictionary2[key];
				if ((object)propertyInfo.PropertyType == propertyInfo2.PropertyType && propertyInfo2.CanWrite && propertyInfo.CanRead)
				{
					propertyInfo2.SetValue(P_1, propertyInfo.GetValue(P_0, null), null);
				}
			}
		}
	}
}
