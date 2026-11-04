using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using SynapseGaming.LightingSystem.Serialization;

namespace W;

internal class g
{
	private Dictionary<Type, string> fNB = new Dictionary<Type, string>(16);

	private Dictionary<Type, MemberInfo[]> fNa = new Dictionary<Type, MemberInfo[]>(16);

	private List<MemberInfo> fNg = new List<MemberInfo>(16);

	internal Dictionary<Type, string> TypeNames => fNB;

	internal void _0013()
	{
		fNa.Clear();
		fNB.Clear();
	}

	internal void n_0003(Type P_0, out string P_1, out MemberInfo[] P_2)
	{
		if (fNB.ContainsKey(P_0))
		{
			P_1 = fNB[P_0];
			P_2 = fNa[P_0];
			return;
		}
		int num = 0;
		string text = (P_1 = XmlConvert.EncodeLocalName(P_0.Name));
		while (fNB.ContainsValue(P_1))
		{
			P_1 = text + "_" + num++;
		}
		P_2 = a.na(P_0);
		object[] customAttributes = P_0.GetCustomAttributes(typeof(SerializationInclusionModelAttribute), inherit: true);
		if (customAttributes != null && customAttributes.Length > 0)
		{
			fNg.Clear();
			MemberInfo[] array = P_2;
			foreach (MemberInfo memberInfo in array)
			{
				customAttributes = memberInfo.GetCustomAttributes(typeof(SerializeMemberAttribute), inherit: true);
				if (customAttributes != null && customAttributes.Length > 0)
				{
					fNg.Add(memberInfo);
				}
			}
			P_2 = fNg.ToArray();
		}
		fNB.Add(P_0, P_1);
		fNa.Add(P_0, P_2);
	}
}
