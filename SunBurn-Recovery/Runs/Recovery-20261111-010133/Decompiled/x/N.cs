using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using SynapseGaming.LightingSystem.Serialization;

namespace x
{
	[Flags]
	internal enum N
	{
		LightingEffect = 1,
		BasicEffect_Lighting = 2,
		BasicEffect_NonLighting = 4,
		MiscEffect = 8
	}
}
namespace X
{
	internal class N
	{
		private Dictionary<Type, string> l8a = new Dictionary<Type, string>(16);

		private Dictionary<Type, MemberInfo[]> l8_u0013_ = new Dictionary<Type, MemberInfo[]>(16);

		private List<MemberInfo> l8N = new List<MemberInfo>(16);

		internal Dictionary<Type, string> TypeNames => l8a;

		internal void _id_3()
		{
			l8_u0013_.Clear();
			l8a.Clear();
		}

		internal void Lg(Type param_0, out string param_1, out MemberInfo[] param_2)
		{
			if (l8a.ContainsKey(param_0))
			{
				param_1 = l8a[param_0];
				param_2 = l8_u0013_[param_0];
				return;
			}
			int num = 0;
			string text = (param_1 = XmlConvert.EncodeLocalName(param_0.Name));
			while (l8a.ContainsValue(param_1))
			{
				param_1 = text + "_" + num++;
			}
			param_2 = _u0013_.Lt(param_0);
			object[] customAttributes = param_0.GetCustomAttributes(typeof(SerializationInclusionModelAttribute), inherit: true);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				l8N.Clear();
				MemberInfo[] array = param_2;
				foreach (MemberInfo memberInfo in array)
				{
					customAttributes = memberInfo.GetCustomAttributes(typeof(SerializeMemberAttribute), inherit: true);
					if (customAttributes != null && customAttributes.Length > 0)
					{
						l8N.Add(memberInfo);
					}
				}
				param_2 = l8N.ToArray();
			}
			l8a.Add(param_0, param_1);
			l8_u0013_.Add(param_0, param_2);
		}
	}
}
