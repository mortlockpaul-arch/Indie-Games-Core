using System;
using System.Collections.Generic;
using System.Reflection;
using L;
using SynapseGaming.LightingSystem.Serialization;

namespace x
{
	internal class _u0013_
	{
		internal const string l8a = "Entities";

		internal const string l8_u0013_ = "EntityGroups";

		internal const string l8N = "LightGroups";
	}
}
namespace X
{
	internal class _u0013_
	{
		internal static MemberInfo[] Lt(Type param_0)
		{
			object[] customAttributes = param_0.GetCustomAttributes(typeof(SerializationInclusionModelAttribute), inherit: true);
			if (customAttributes == null || customAttributes.Length < 1)
			{
				return global::L._u0013_.GetSerializableMembers(param_0);
			}
			return L4(param_0);
		}

		private static MemberInfo[] L4(Type param_0)
		{
			MemberInfo[] members = param_0.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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
}
