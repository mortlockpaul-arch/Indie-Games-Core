using System;
using System.Collections.Generic;
using System.Reflection;
using SynapseGaming.LightingSystem.Serialization;
using n;

namespace W;

internal class a
{
	internal static MemberInfo[] na(Type P_0)
	{
		object[] customAttributes = P_0.GetCustomAttributes(typeof(SerializationInclusionModelAttribute), inherit: true);
		if (customAttributes == null || customAttributes.Length < 1)
		{
			return n.a.GetSerializableMembers(P_0);
		}
		return ng(P_0);
	}

	private static MemberInfo[] ng(Type P_0)
	{
		MemberInfo[] members = P_0.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		List<MemberInfo> list = new List<MemberInfo>(members.Length);
		MemberInfo[] array = members;
		foreach (MemberInfo memberInfo in array)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if ((object)propertyInfo != null && propertyInfo.CanRead && propertyInfo.CanWrite)
			{
				list.Add(memberInfo);
			}
			else if ((object)fieldInfo != null)
			{
				list.Add(memberInfo);
			}
		}
		return list.ToArray();
	}
}
