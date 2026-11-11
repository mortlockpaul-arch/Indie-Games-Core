using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Serialization;
using X;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public class ContentRepository : IFullSerializable, ISerializable, INamedObject, IDisposable
{
	[Serializable]
	public class BaseAssetData : IFullSerializable, ISerializable
	{
		public string PipelineImporterClassName = string.Empty;

		public string PipelineProcessorClassName = string.Empty;

		public Dictionary<string, string> PipelineProcessorOptions = new Dictionary<string, string>();

		internal virtual PipelineAssetType PipelineAssetType => PipelineAssetType.None;

		public void SetObjectData(SerializationInfo info, StreamingContext context)
		{
			PipelineProcessorOptions.Clear();
			SerializationHelper.DeserializeField(ref PipelineImporterClassName, info, "PipelineImporterClassName", usedefault: true);
			SerializationHelper.DeserializeField(ref PipelineProcessorClassName, info, "PipelineProcessorClassName", usedefault: true);
			SerializationHelper.DeserializeField(ref PipelineProcessorOptions, info, "PipelineProcessorOptions", usedefault: false);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationHelper.SerializeFieldOrEnum(ref PipelineImporterClassName, info, "PipelineImporterClassName");
			SerializationHelper.SerializeFieldOrEnum(ref PipelineProcessorClassName, info, "PipelineProcessorClassName");
			SerializationHelper.SerializeFieldOrEnum(ref PipelineProcessorOptions, info, "PipelineProcessorOptions");
		}

		internal void _id_8(ContentReader param_0)
		{
			PipelineImporterClassName = param_0.ReadString();
			PipelineProcessorClassName = param_0.ReadString();
			PipelineProcessorOptions = param_0.ReadObject<Dictionary<string, string>>();
		}
	}

	[Serializable]
	public class SoundEffectData : BaseAssetData
	{
		internal override PipelineAssetType PipelineAssetType => PipelineAssetType.Sound;
	}

	[Serializable]
	public class ModelData : BaseAssetData
	{
		internal List<string> l8a = new List<string>();

		internal override PipelineAssetType PipelineAssetType => PipelineAssetType.Model;

		internal ModelData()
		{
		}

		internal ModelData(List<string> param_0)
		{
			l8a = param_0;
		}
	}

	public const string LightMapCachePath = "LightMapCache\\";

	private static ContentRepository l8a;

	private static Dictionary<string, ContentRepository> l8_u0013_ = new Dictionary<string, ContentRepository>(4);

	private string l8N = "";

	private string l8l = "";

	private string l8L = "";

	private string l8w = "";

	private ProcessorRenderingType l8J;

	private ContentManager l8B;

	private Dictionary<string, ModelData> l8s = new Dictionary<string, ModelData>();

	private Dictionary<string, SoundEffectData> l8h = new Dictionary<string, SoundEffectData>();

	private List<string> l8_u0002_ = new List<string>();

	private List<string> l8v = new List<string>();

	private Dictionary<string, PrefabObjectGenerator> l8Y = new Dictionary<string, PrefabObjectGenerator>();

	private Dictionary<string, string> l8_u0005_ = new Dictionary<string, string>();

	public string Name
	{
		get
		{
			return l8N;
		}
		set
		{
		}
	}

	internal string FileName
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
		}
	}

	internal string XnbContentManagerFileName
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
		}
	}

	internal ProcessorRenderingType ProcessorRenderingType
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	public static ContentRepository DefaultContentRepository => l8a;

	public static Dictionary<string, ContentRepository> ContentRepositories => l8_u0013_;

	internal Dictionary<string, ModelData> Models => l8s;

	internal Dictionary<string, SoundEffectData> SoundEffects => l8h;

	internal List<string> LightMaps => l8_u0002_;

	internal List<string> LightOcclusionBuffers => l8v;

	internal Dictionary<string, PrefabObjectGenerator> Prefabs => l8Y;

	private static void _u0006_(ContentRepository param_0)
	{
		string name = param_0.Name;
		if (l8_u0013_.ContainsKey(name))
		{
			throw new Exception($"Content repository named '{name}' already exists.");
		}
		l8_u0013_.Add(name, param_0);
		if (l8a == null)
		{
			l8a = param_0;
		}
	}

	private static void e(ContentRepository param_0)
	{
		l8_u0013_.Remove(param_0.Name);
		if (l8a != param_0)
		{
			return;
		}
		l8a = null;
		using Dictionary<string, ContentRepository>.Enumerator enumerator = l8_u0013_.GetEnumerator();
		if (enumerator.MoveNext())
		{
			l8a = enumerator.Current.Value;
		}
	}

	public static ContentRepository Find(string contentrepositoryname)
	{
		if (string.IsNullOrEmpty(contentrepositoryname))
		{
			return null;
		}
		if (l8_u0013_.TryGetValue(contentrepositoryname, out var value))
		{
			return value;
		}
		return null;
	}

	internal ContentRepository(string param_0, ContentManager param_1)
	{
		l8N = param_0;
		l8B = param_1;
		_u0006_(this);
	}

	public ContentRepository()
	{
	}

	public void Dispose()
	{
		e(this);
	}

	internal void _u0010_(string param_0, string param_1, ModelData param_2)
	{
		if (!l8_u0005_.ContainsKey(param_0))
		{
			l8s.Add(param_0, param_2);
			l8_u0005_.Add(param_0, param_1);
		}
	}

	internal void V(string param_0)
	{
		l8s.Remove(param_0);
		l8_u0005_.Remove(param_0);
	}

	internal void _u0011_(string param_0, string param_1, SoundEffectData param_2)
	{
		if (!l8_u0005_.ContainsKey(param_0))
		{
			l8h.Add(param_0, param_2);
			l8_u0005_.Add(param_0, param_1);
		}
	}

	internal void _u0003_(string param_0)
	{
		l8h.Remove(param_0);
		l8_u0005_.Remove(param_0);
	}

	internal void m(string param_0, string param_1)
	{
		if (!l8_u0005_.ContainsKey(param_0))
		{
			l8_u0002_.Add(param_0);
			l8_u0005_.Add(param_0, param_1);
		}
	}

	internal void n(string param_0, string param_1)
	{
		if (!l8_u0005_.ContainsKey(param_0))
		{
			l8v.Add(param_0);
			l8_u0005_.Add(param_0, param_1);
		}
	}

	internal void k(string param_0, PrefabObjectGenerator param_1)
	{
		if (l8Y.ContainsKey(param_0))
		{
			l8Y[param_0] = param_1;
		}
		else
		{
			l8Y.Add(param_0, param_1);
		}
	}

	internal string P(int param_0)
	{
		return string.Format("{0}{1}.lm", "LightMapCache\\", param_0);
	}

	internal string D(int param_0)
	{
		return string.Format("{0}{1}.om", "LightMapCache\\", param_0);
	}

	public LightMap LoadLightMap(RenderableMesh mesh)
	{
		return LoadBySourceAssetPath<LightMap>(P(mesh.l8N), allownull: true);
	}

	public LightOcclusionBuffer LoadLightOcclusionBuffer(ILight light)
	{
		return LoadBySourceAssetPath<LightOcclusionBuffer>(D(light.UniqueId), allownull: true);
	}

	public PrefabObjectGenerator LoadPrefab(string name)
	{
		if (l8Y.TryGetValue(name, out var value))
		{
			return value;
		}
		return null;
	}

	public T LoadBySourceAssetPath<T>(string sourceassetpath, bool allownull)
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, string> item in l8_u0005_)
		{
			if (item.Key.Equals(sourceassetpath, StringComparison.InvariantCultureIgnoreCase))
			{
				text = item.Value;
				break;
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			if (allownull)
			{
				return default;
			}
			throw new Exception($"Asset with source path '{sourceassetpath}' does not exist in the repository.");
		}
		return Load<T>(text);
	}

	public T Load<T>(string xnbassetpath)
	{
		if (string.IsNullOrEmpty(xnbassetpath))
		{
			return default;
		}
		return l8B.Load<T>(xnbassetpath);
	}

	public void Clear()
	{
		l8s.Clear();
		l8h.Clear();
		l8_u0005_.Clear();
		l8_u0002_.Clear();
		l8v.Clear();
	}

	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		l8s = X.L.Lc(info);
		SerializationHelper.DeserializeField(ref l8h, info, "SoundEffects", usedefault: true);
		if (l8h == null)
		{
			l8h = new Dictionary<string, SoundEffectData>();
		}
		SerializationHelper.DeserializeField(ref l8_u0002_, info, "LightMaps", usedefault: false);
		SerializationHelper.DeserializeField(ref l8v, info, "LightOcclusionBuffers", usedefault: false);
		SerializationHelper.DeserializeField(ref l8Y, info, "Prefabs", usedefault: false);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.SerializeFieldOrEnum(ref l8s, info, "Models");
		SerializationHelper.SerializeFieldOrEnum(ref l8h, info, "SoundEffects");
		SerializationHelper.SerializeFieldOrEnum(ref l8_u0002_, info, "LightMaps");
		SerializationHelper.SerializeFieldOrEnum(ref l8v, info, "LightOcclusionBuffers");
		SerializationHelper.SerializeFieldOrEnum(ref l8Y, info, "Prefabs");
	}
}
