using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;

namespace x
{
	[Flags]
	internal enum l
	{
		None = 1,
		Skinned = 2
	}
	internal class L : IComparer<RenderableMesh>
	{
		public int Compare(RenderableMesh a, RenderableMesh b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return a.l8Z - b.l8Z;
		}
	}
}
namespace X
{
	internal class l
	{
		private Dictionary<Type, MemberInfo[]> l8a = new Dictionary<Type, MemberInfo[]>(16);

		private Dictionary<string, Type> l8_u0013_ = new Dictionary<string, Type>(16);

		internal void L_u0006_()
		{
			L_u000E_(typeof(ContentRepository));
			L_u000E_(typeof(Vector2));
			L_u000E_(typeof(Vector3));
			L_u000E_(typeof(Vector4));
			L_u000E_(typeof(Matrix));
			L_u000E_(typeof(Scene));
			L_u000E_(typeof(SceneEnvironment));
		}

		internal void _id_3()
		{
			l8a.Clear();
			l8_u0013_.Clear();
		}

		internal Type Le(string param_0, string param_1, string param_2)
		{
			if (l8_u0013_.TryGetValue(param_2, out var value))
			{
				return value;
			}
			value = LC(param_0, param_1, param_2);
			if ((object)value == null)
			{
				throw new Exception($"Type '{param_0}' not registered with the serialization type dictionary.");
			}
			L_u000E_(param_2, value);
			return value;
		}

		internal void _u0006_(string param_0, Type param_1)
		{
			if (!string.IsNullOrEmpty(param_0) && !l8_u0013_.ContainsKey(param_0))
			{
				L_u000E_(param_0, param_1);
			}
		}

		internal void LU(string param_0, out Type param_1, out MemberInfo[] param_2)
		{
			if (string.IsNullOrEmpty(param_0) || !l8_u0013_.ContainsKey(param_0))
			{
				throw new Exception("Type '" + param_0 + "' not registered with the serialization type dictionary.");
			}
			param_1 = l8_u0013_[param_0];
			param_2 = l8a[param_1];
		}

		private void L_u000E_(Type param_0)
		{
			string name = param_0.Name;
			if (!string.IsNullOrEmpty(name) && !l8_u0013_.ContainsKey(name))
			{
				L_u000E_(name, param_0);
			}
		}

		private void L_u000E_(string param_0, Type param_1)
		{
			l8_u0013_.Add(param_0, param_1);
			MemberInfo[] value = ((!param_1.IsGenericTypeDefinition) ? _u0013_.Lt(param_1) : new MemberInfo[0]);
			if (!l8a.ContainsKey(param_1))
			{
				l8a.Add(param_1, value);
			}
		}

		internal static Type LC(string param_0, string param_1, string param_2)
		{
			Type type = Type.GetType($"{param_0}, {param_1}");
			if ((object)type != null)
			{
				return type;
			}
			type = Type.GetType(param_0);
			if ((object)type != null)
			{
				return type;
			}
			param_0 = L1(param_0);
			type = Type.GetType(param_0);
			if ((object)type != null)
			{
				return type;
			}
			return null;
		}

		private static string L1(string param_0)
		{
			if (param_0.StartsWith("SynapseGaming.LightingSystem.Core.IComponent"))
			{
				return param_0.Replace("SynapseGaming.LightingSystem.Core.IComponent", "SynapseGaming.LightingSystem.Components.IComponent");
			}
			return param_0;
		}
	}
	internal class L
	{
		internal static Dictionary<string, ContentRepository.ModelData> Lc(SerializationInfo param_0)
		{
			Dictionary<string, ContentRepository.ModelData> dictionary = new Dictionary<string, ContentRepository.ModelData>();
			try
			{
				dictionary = (Dictionary<string, ContentRepository.ModelData>)param_0.GetValue("Models", typeof(Dictionary<string, ContentRepository.ModelData>));
			}
			catch
			{
			}
			try
			{
				List<string> list = (List<string>)param_0.GetValue("Models", typeof(List<string>));
				foreach (string item in list)
				{
					dictionary.Add(item, new ContentRepository.ModelData());
				}
			}
			catch
			{
			}
			return dictionary;
		}

		internal static UpdateType L_u0010_(SerializationInfo param_0)
		{
			try
			{
				string value = (string)param_0.GetValue("UpdateType", typeof(string));
				return (UpdateType)Enum.Parse(typeof(UpdateType), value, ignoreCase: false);
			}
			catch
			{
			}
			try
			{
				string text = (string)param_0.GetValue("ObjectType", typeof(string));
				if (text == "Static")
				{
					return UpdateType.None;
				}
				if (text == "Dynamic")
				{
					return UpdateType.Automatic;
				}
			}
			catch
			{
			}
			return UpdateType.None;
		}

		internal static StaticLightingType LV(SerializationInfo param_0)
		{
			try
			{
				string value = (string)param_0.GetValue("StaticLightingType", typeof(string));
				return (StaticLightingType)Enum.Parse(typeof(StaticLightingType), value, ignoreCase: false);
			}
			catch
			{
			}
			try
			{
				if ((bool)param_0.GetValue("LightMapped", typeof(bool)))
				{
					return StaticLightingType.BakedDown;
				}
				return StaticLightingType.None;
			}
			catch
			{
			}
			return StaticLightingType.None;
		}
	}
}
