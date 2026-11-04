using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using L;
using SynapseGaming.LightingSystem.Audio;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;
using h;
using v;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Base container object used for storing, sharing, and organizing scene entities, objects, and lights.
/// </summary>
[Serializable]
public abstract class BaseScene : IScene, IEditorActiveObject, IEditorObject, INamedObject, IDisposable, L.v, IFullSerializable, ISerializable
{
	private string fNB = "";

	private string fNa = "";

	private string fNg = "";

	/// <summary />
	protected List<ILightGroup> _LightGroups = new List<ILightGroup>(16);

	/// <summary />
	protected List<ISceneEntityGroup> _EntityGroups = new List<ISceneEntityGroup>(16);

	private bool fNf;

	private h.B<IAudioManager, AudioSource> fN_0003 = new h.B<IAudioManager, AudioSource>();

	private h.B<IObjectManager, SceneEntity> fNn = new h.B<IObjectManager, SceneEntity>();

	private h.B<ILightManager, BaseLight> fN_0019 = new h.B<ILightManager, BaseLight>();

	[CompilerGenerated]
	private bool fNL;

	[CompilerGenerated]
	private bool fNv;

	/// <summary>
	/// Light groups contained by the scene.
	/// </summary>
	public List<ILightGroup> LightGroups => _LightGroups;

	/// <summary>
	/// Scene object groups contained by the scene.
	/// </summary>
	public List<ISceneEntityGroup> EntityGroups => _EntityGroups;

	/// <summary>
	/// The object's current name.
	/// </summary>
	public string Name
	{
		get
		{
			return fNB;
		}
		set
		{
		}
	}

	/// <summary>
	/// Notifies the editor that the object is currently used for rendering. The editor
	/// will display unused / inactive objects as grayed-out.
	/// </summary>
	public bool AssetInUse
	{
		[CompilerGenerated]
		get
		{
			return fNL;
		}
		[CompilerGenerated]
		internal set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Notifies the editor that the object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fNv;
		}
		[CompilerGenerated]
		set
		{
			fNv = value;
		}
	}

	internal string FileName
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	string L.v.ProjectFile => fNg;

	internal void gn(string P_0)
	{
		fNB = P_0;
	}

	/// <summary>
	/// Creates a BaseScene instance.
	/// </summary>
	public BaseScene()
		: this(true)
	{
	}

	/// <summary>
	/// Creates a BaseScene instance.
	/// </summary>
	protected BaseScene(bool P_0)
	{
		if (P_0)
		{
			LightingSystemEditor.OnCreateResource(this);
		}
	}

	/// <summary>
	/// Releases resources allocated by this object.
	/// </summary>
	public void Dispose()
	{
		Clear();
		if (!fNf)
		{
			fNf = true;
			LightingSystemEditor.OnDisposeResource(this);
		}
	}

	/// <summary>
	/// Sets the single manager of the specified type that contains this scene.
	///
	/// Scenes can only be contained by a single manager of a specific type.
	/// </summary>
	/// <typeparam name="T">Type of the specified manager. This is often the
	/// manager interface type, not the class type.</typeparam>
	/// <param name="manager">Manager object that contains the scene.</param>
	public void SetContainingManager<T>(IManager manager)
	{
		if ((object)typeof(T) == typeof(IAudioManager))
		{
			fN_0003.SetManager(manager as IAudioManager);
		}
		else if ((object)typeof(T) == typeof(IObjectManager))
		{
			fNn.SetManager(manager as IObjectManager);
		}
		else if ((object)typeof(T) == typeof(ILightManager))
		{
			fN_0019.SetManager(manager as ILightManager);
		}
		AssetInUse = fN_0003.ContainingManager != null || fNn.ContainingManager != null || fN_0019.ContainingManager != null;
		LightingSystemEditor.gJ(this, v.g.InUse);
	}

	/// <summary>
	/// Removes all objects and groups.
	/// </summary>
	public void Clear()
	{
		_LightGroups.Clear();
		_EntityGroups.Clear();
		fN_0003.RemoveSubmittedObjects();
		fNn.RemoveSubmittedObjects();
		fN_0019.RemoveSubmittedObjects();
	}

	/// <summary>
	/// Applies changes made to contained objects and groups.  This must be called after
	/// making changes and before rendering the scene.
	/// </summary>
	public void Apply()
	{
		fN_0003.RemoveSubmittedObjects();
		fNn.RemoveSubmittedObjects();
		fN_0019.RemoveSubmittedObjects();
		foreach (ISceneEntityGroup entityGroup in _EntityGroups)
		{
			for (int k = 0; k < entityGroup.Entities.Count; k++)
			{
				ISceneEntity sceneEntity = entityGroup.Entities[k];
				if (sceneEntity is SceneEntity)
				{
					fNn.Submit(sceneEntity as SceneEntity);
				}
				else if (sceneEntity is AudioSource)
				{
					fN_0003.Submit(sceneEntity as AudioSource);
				}
			}
		}
		foreach (ILightGroup lightGroup3 in _LightGroups)
		{
			if (lightGroup3.ShadowGroup && lightGroup3.ShadowRenderLightsTogether)
			{
				int num = 0;
				ILightGroup lightGroup = lightGroup3;
				for (int l = 0; l < lightGroup3.Lights.Count; l++)
				{
					if (num >= BaseLightManager.MaxLightsPerGroup)
					{
						LightGroup lightGroup2 = new LightGroup();
						lightGroup2.fy(lightGroup);
						lightGroup = lightGroup2;
						num = 0;
					}
					ILight light = lightGroup3.Lights[l];
					if (light is BaseLight obj)
					{
						light.ShadowSource = lightGroup;
						fN_0019.Submit(obj);
						num++;
					}
				}
				continue;
			}
			for (int m = 0; m < lightGroup3.Lights.Count; m++)
			{
				if (lightGroup3.Lights[m] is BaseLight obj2)
				{
					fN_0019.Submit(obj2);
				}
			}
		}
		fN_0003.Optimize();
		fNn.Optimize();
		fN_0019.Optimize();
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public abstract void SetObjectData(SerializationInfo info, StreamingContext context);

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
}
