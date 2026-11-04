using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace W;

internal class f
{
	private Dictionary<Type, MemberInfo[]> fNB = new Dictionary<Type, MemberInfo[]>(16);

	private Dictionary<string, Type> fNa = new Dictionary<string, Type>(16);

	internal void nn()
	{
		nv(typeof(ContentRepository));
		nv(typeof(Vector2));
		nv(typeof(Vector3));
		nv(typeof(Vector4));
		nv(typeof(Matrix));
		nv(typeof(Scene));
		nv(typeof(SceneEnvironment));
	}

	internal void _0013()
	{
		fNB.Clear();
		fNa.Clear();
	}

	internal Type n_0019(string P_0, string P_1, string P_2)
	{
		if (fNa.TryGetValue(P_2, out var value))
		{
			return value;
		}
		value = n_0012(P_0, P_1, P_2);
		if ((object)value == null)
		{
			throw new Exception($"Type '{P_0}' not registered with the serialization type dictionary.");
		}
		nv(P_2, value);
		return value;
	}

	internal void k(string P_0, Type P_1)
	{
		if (!string.IsNullOrEmpty(P_0) && !fNa.ContainsKey(P_0))
		{
			nv(P_0, P_1);
		}
	}

	internal void nL(string P_0, out Type P_1, out MemberInfo[] P_2)
	{
		if (string.IsNullOrEmpty(P_0) || !fNa.ContainsKey(P_0))
		{
			throw new Exception("Type '" + P_0 + "' not registered with the serialization type dictionary.");
		}
		P_1 = fNa[P_0];
		P_2 = fNB[P_1];
	}

	private void nv(Type P_0)
	{
		string name = P_0.Name;
		if (!string.IsNullOrEmpty(name) && !fNa.ContainsKey(name))
		{
			nv(name, P_0);
		}
	}

	private void nv(string P_0, Type P_1)
	{
		fNa.Add(P_0, P_1);
		MemberInfo[] value = ((!P_1.IsGenericTypeDefinition) ? a.na(P_1) : new MemberInfo[0]);
		if (!fNB.ContainsKey(P_1))
		{
			fNB.Add(P_1, value);
		}
	}

	internal static Type n_0012(string P_0, string P_1, string P_2)
	{
		Type type = Type.GetType($"{P_0}, {P_1}");
		if ((object)type != null)
		{
			return type;
		}
		type = Type.GetType(P_0);
		if ((object)type != null)
		{
			return type;
		}
		return null;
	}
}
