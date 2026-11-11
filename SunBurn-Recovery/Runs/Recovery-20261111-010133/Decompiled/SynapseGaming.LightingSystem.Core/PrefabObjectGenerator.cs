using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public class PrefabObjectGenerator : IFullSerializable, ISerializable
{
	private PrefabObjectCategory l8a;

	private string l8_u0013_ = "";

	private object[] l8N = new object[0];

	private Type[] l8l = new Type[0];

	public PrefabObjectCategory Category => l8a;

	public string Xml => l8_u0013_;

	public PrefabObjectGenerator()
	{
	}

	public PrefabObjectGenerator(string xml)
	{
		l8_u0013_ = xml;
	}

	internal PrefabObjectGenerator(string param_0, PrefabObjectCategory param_1)
	{
		l8_u0013_ = param_0;
		l8a = param_1;
	}

	public object CreateObject()
	{
		return CreateObject<object>();
	}

	public T CreateObject<T>() where T : class
	{
		T val = SerializationHelper.LoadFromXml<T>(l8_u0013_);
		if (val == null)
		{
			return val;
		}
		Type type = val.GetType();
		MethodInfo method = type.GetMethod("Clone", l8l);
		if ((object)method != null)
		{
			try
			{
				if (method.Invoke(val, l8N) is T val2)
				{
					val = val2;
				}
			}
			catch
			{
			}
		}
		return val;
	}

	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref l8a, info, "Category", isflag: false);
		SerializationHelper.DeserializeField(ref l8_u0013_, info, "Data", usedefault: true);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.SerializeFieldOrEnum(ref l8a, info, "Category");
		SerializationHelper.SerializeFieldOrEnum(ref l8_u0013_, info, "Data");
	}
}
