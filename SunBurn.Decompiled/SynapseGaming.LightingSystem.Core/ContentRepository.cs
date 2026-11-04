using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Serialization;
using W;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides storage of automatically loaded game content. This includes
/// models, light maps, and light occlusion buffers loaded with scenes
/// and during rendering.
///
/// Content repositories must be loaded via a content manager before
/// scenes and other objects referencing their contents are loaded.
/// </summary>
[Serializable]
public class ContentRepository : IFullSerializable, ISerializable, INamedObject, IDisposable
{
	/// <summary>
	/// Used internally.
	/// </summary>
	[Serializable]
	public class BaseAssetData : IFullSerializable, ISerializable
	{
		internal string fNB = string.Empty;

		internal string fNa = string.Empty;

		internal Dictionary<string, string> fNg = new Dictionary<string, string>();

		internal virtual PipelineAssetType PipelineAssetType => PipelineAssetType.None;

		/// <summary>
		/// Deserializes object data from the provided SerializationInfo.
		/// </summary>
		/// <param name="info">Contains the serialized object data.</param>
		/// <param name="context"></param>
		public void SetObjectData(SerializationInfo info, StreamingContext context)
		{
			fNg.Clear();
			SerializationHelper.DeserializeField(ref fNB, info, "PipelineImporterClassName", usedefault: true);
			SerializationHelper.DeserializeField(ref fNa, info, "PipelineProcessorClassName", usedefault: true);
			SerializationHelper.DeserializeField(ref fNg, info, "PipelineProcessorOptions", usedefault: false);
		}

		/// <summary>
		/// Serializes object data to the provided SerializationInfo.
		/// </summary>
		/// <param name="info">SerializationInfo to store the serialized data.</param>
		/// <param name="context"></param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationHelper.SerializeFieldOrEnum(ref fNB, info, "PipelineImporterClassName");
			SerializationHelper.SerializeFieldOrEnum(ref fNa, info, "PipelineProcessorClassName");
			SerializationHelper.SerializeFieldOrEnum(ref fNg, info, "PipelineProcessorOptions");
		}

		internal void av(ContentReader P_0)
		{
			fNB = P_0.ReadString();
			fNa = P_0.ReadString();
			fNg = P_0.ReadObject<Dictionary<string, string>>();
		}
	}

	/// <summary>
	/// Used internally.
	/// </summary>
	[Serializable]
	public class SoundEffectData : BaseAssetData
	{
		internal override PipelineAssetType PipelineAssetType => PipelineAssetType.Sound;
	}

	/// <summary>
	/// Used internally.
	/// </summary>
	[Serializable]
	public class ModelData : BaseAssetData
	{
		internal new List<string> fNB = new List<string>();

		internal override PipelineAssetType PipelineAssetType => PipelineAssetType.Model;

		internal ModelData()
		{
		}

		internal ModelData(List<string> P_0)
		{
			fNB = P_0;
		}
	}

	/// <summary>
	/// Relative path to the light map cache directory.
	/// </summary>
	public const string LightMapCachePath = "LightMapCache\\";

	private static ContentRepository fNB;

	private static Dictionary<string, ContentRepository> fNa = new Dictionary<string, ContentRepository>(4);

	private string fNg = "";

	private string fNf = "";

	private string fN_0003 = "";

	private string fNn = "";

	private ProcessorRenderingType fN_0019;

	private ContentManager fNL;

	private Dictionary<string, ModelData> fNv = new Dictionary<string, ModelData>();

	private Dictionary<string, SoundEffectData> fN_0012 = new Dictionary<string, SoundEffectData>();

	private List<string> fN_0015 = new List<string>();

	private List<string> fN8 = new List<string>();

	private Dictionary<string, PrefabObjectGenerator> fNi = new Dictionary<string, PrefabObjectGenerator>();

	private Dictionary<string, string> fN_0005 = new Dictionary<string, string>();

	/// <summary>
	/// The object's current name.
	/// </summary>
	public string Name
	{
		get
		{
			return fNg;
		}
		set
		{
		}
	}

	internal string FileName
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = value;
		}
	}

	internal string XnbContentManagerFileName
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	internal ProcessorRenderingType ProcessorRenderingType
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Gets the default content repository. This may be null if no content repositories are loaded.
	/// </summary>
	public static ContentRepository DefaultContentRepository => fNB;

	/// <summary>
	/// List of all content repositories.
	/// </summary>
	public static Dictionary<string, ContentRepository> ContentRepositories => fNa;

	internal Dictionary<string, ModelData> Models => fNv;

	internal Dictionary<string, SoundEffectData> SoundEffects => fN_0012;

	internal List<string> LightMaps => fN_0015;

	internal List<string> LightOcclusionBuffers => fN8;

	internal Dictionary<string, PrefabObjectGenerator> Prefabs => fNi;

	private static void k(ContentRepository P_0)
	{
		string name = P_0.Name;
		if (fNa.ContainsKey(name))
		{
			throw new Exception($"Content repository named '{name}' already exists.");
		}
		fNa.Add(name, P_0);
		if (fNB == null)
		{
			fNB = P_0;
		}
	}

	private static void C(ContentRepository P_0)
	{
		fNa.Remove(P_0.Name);
		if (fNB != P_0)
		{
			return;
		}
		fNB = null;
		using Dictionary<string, ContentRepository>.Enumerator enumerator = fNa.GetEnumerator();
		if (enumerator.MoveNext())
		{
			fNB = enumerator.Current.Value;
		}
	}

	/// <summary>
	/// Finds a content repository by name.
	/// </summary>
	/// <param name="contentrepositoryname">Name of the content manager to find.</param>
	/// <returns></returns>
	public static ContentRepository Find(string contentrepositoryname)
	{
		if (string.IsNullOrEmpty(contentrepositoryname))
		{
			return null;
		}
		if (fNa.TryGetValue(contentrepositoryname, out var value))
		{
			return value;
		}
		return null;
	}

	internal ContentRepository(string P_0, ContentManager P_1)
	{
		fNg = P_0;
		fNL = P_1;
		k(this);
	}

	/// <summary>
	/// Only for serialization. Using this constructor in game code may cause an exception.
	/// </summary>
	public ContentRepository()
	{
	}

	/// <summary>
	/// Disposes the content repository. This removes it from the list of available repositories.
	/// </summary>
	public void Dispose()
	{
		C(this);
	}

	internal void _0018(string P_0, string P_1, ModelData P_2)
	{
		if (!fN_0005.ContainsKey(P_0))
		{
			fNv.Add(P_0, P_2);
			fN_0005.Add(P_0, P_1);
		}
	}

	internal void N(string P_0)
	{
		fNv.Remove(P_0);
		fN_0005.Remove(P_0);
	}

	internal void aB(string P_0, string P_1, SoundEffectData P_2)
	{
		if (!fN_0005.ContainsKey(P_0))
		{
			fN_0012.Add(P_0, P_2);
			fN_0005.Add(P_0, P_1);
		}
	}

	internal void aa(string P_0)
	{
		fN_0012.Remove(P_0);
		fN_0005.Remove(P_0);
	}

	internal void ag(string P_0, string P_1)
	{
		if (!fN_0005.ContainsKey(P_0))
		{
			fN_0015.Add(P_0);
			fN_0005.Add(P_0, P_1);
		}
	}

	internal void af(string P_0, string P_1)
	{
		if (!fN_0005.ContainsKey(P_0))
		{
			fN8.Add(P_0);
			fN_0005.Add(P_0, P_1);
		}
	}

	internal void a_0003(string P_0, PrefabObjectGenerator P_1)
	{
		if (fNi.ContainsKey(P_0))
		{
			fNi[P_0] = P_1;
		}
		else
		{
			fNi.Add(P_0, P_1);
		}
	}

	internal string an(int P_0)
	{
		return string.Format("{0}{1}.lm", "LightMapCache\\", P_0);
	}

	internal string a_0019(int P_0)
	{
		return string.Format("{0}{1}.om", "LightMapCache\\", P_0);
	}

	/// <summary>
	/// Loads the light map associated with a RenderableMesh.
	/// </summary>
	/// <param name="mesh"></param>
	/// <returns></returns>
	public Texture2D LoadLightMap(RenderableMesh mesh)
	{
		return LoadBySourceAssetPath<Texture2D>(an(mesh.fNg), allownull: true);
	}

	/// <summary>
	/// Loads the light occlusion buffer associated with a directional light.
	/// </summary>
	/// <param name="light"></param>
	/// <returns></returns>
	public LightOcclusionBuffer LoadLightOcclusionBuffer(ILight light)
	{
		return LoadBySourceAssetPath<LightOcclusionBuffer>(a_0019(light.UniqueId), allownull: true);
	}

	/// <summary>
	/// Loads an asset using the relative source path. The path includes
	/// the original file extension.
	///
	/// For instance: "models\\chair.fbx"
	/// </summary>
	/// <typeparam name="T">Type of returned class.</typeparam>
	/// <param name="sourceassetpath">Asset relative source path.</param>
	/// <param name="allownull">Determines if an exception should
	/// be thrown when the asset does not exist.</param>
	/// <returns></returns>
	public T LoadBySourceAssetPath<T>(string sourceassetpath, bool allownull)
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, string> item in fN_0005)
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

	/// <summary>
	/// Loads an asset using the xna style path. The path does not include
	/// the file extension.
	///
	/// For instance: "models\\chair"
	/// </summary>
	/// <typeparam name="T">Type of returned class.</typeparam>
	/// <param name="xnbassetpath">Asset path.</param>
	/// <returns></returns>
	public T Load<T>(string xnbassetpath)
	{
		if (string.IsNullOrEmpty(xnbassetpath))
		{
			return default;
		}
		return fNL.Load<T>(xnbassetpath);
	}

	/// <summary>
	/// Removes all objects from the container. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
		fNv.Clear();
		fN_0012.Clear();
		fN_0005.Clear();
		fN_0015.Clear();
		fN8.Clear();
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		fNv = W._0003.n_0015(info);
		SerializationHelper.DeserializeField(ref fN_0012, info, "SoundEffects", usedefault: true);
		if (fN_0012 == null)
		{
			fN_0012 = new Dictionary<string, SoundEffectData>();
		}
		SerializationHelper.DeserializeField(ref fN_0015, info, "LightMaps", usedefault: false);
		SerializationHelper.DeserializeField(ref fN8, info, "LightOcclusionBuffers", usedefault: false);
		SerializationHelper.DeserializeField(ref fNi, info, "Prefabs", usedefault: false);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.SerializeFieldOrEnum(ref fNv, info, "Models");
		SerializationHelper.SerializeFieldOrEnum(ref fN_0012, info, "SoundEffects");
		SerializationHelper.SerializeFieldOrEnum(ref fN_0015, info, "LightMaps");
		SerializationHelper.SerializeFieldOrEnum(ref fN8, info, "LightOcclusionBuffers");
		SerializationHelper.SerializeFieldOrEnum(ref fNi, info, "Prefabs");
	}
}
