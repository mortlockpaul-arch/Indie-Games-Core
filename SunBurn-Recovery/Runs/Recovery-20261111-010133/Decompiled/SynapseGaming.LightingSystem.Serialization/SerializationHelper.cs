using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using X;
using w;

namespace SynapseGaming.LightingSystem.Serialization;

public class SerializationHelper
{
	internal const string l8a = "root";

	internal const string l8_u0013_ = "List";

	internal const string l8N = "classes";

	internal const string l8l = "fullname";

	internal const string l8L = "assembly";

	internal const string l8w = "fullyqualifiedname";

	private static X.a l8J = new X.a();

	public static void SerializeFieldOrEnum<T>(ref T field, SerializationInfo info, string name)
	{
		try
		{
			info.AddValue(name, field);
		}
		catch
		{
		}
	}

	public static void DeserializeField<T>(ref T field, SerializationInfo info, string name, bool usedefault)
	{
		try
		{
			field = (T)info.GetValue(name, typeof(T));
		}
		catch
		{
			if (usedefault)
			{
				field = default;
			}
		}
	}

	public static T DeserializeField<T>(SerializationInfo info, string name)
	{
		try
		{
			return (T)info.GetValue(name, typeof(T));
		}
		catch
		{
		}
		return default;
	}

	public static void DeserializeEnum<T>(ref T field, SerializationInfo info, string name, bool isflag)
	{
		try
		{
			string value = (string)info.GetValue(name, typeof(string));
			field = (T)Enum.Parse(typeof(T), value, ignoreCase: false);
		}
		catch
		{
			if (!isflag)
			{
				field = default;
			}
		}
	}

	public static T DeserializeEnum<T>(SerializationInfo info, string name)
	{
		try
		{
			string value = (string)info.GetValue(name, typeof(string));
			return (T)Enum.Parse(typeof(T), value, ignoreCase: false);
		}
		catch
		{
		}
		return default;
	}

	public static T LoadFromXml<T>(string xml)
	{
		using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
		global::w.w w2 = new global::w.w();
		w2.Load(stream);
		return (T)l8J.LG(w2);
	}
}
