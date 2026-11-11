using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using B;
using SynapseGaming.LightingSystem.Audio;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;
using s;
using x;

namespace SynapseGaming.LightingSystem.Rendering;

[Serializable]
public abstract class BaseScene : IScene, IEditorActiveObject, IEditorObject, INamedObject, IDisposable, B.J, IFullSerializable, ISerializable
{
	private string l8a = "";

	private string l8_u0013_ = "";

	private string l8N = "";

	protected List<ILightGroup> _LightGroups = new List<ILightGroup>(16);

	protected List<ISceneEntityGroup> _EntityGroups = new List<ISceneEntityGroup>(16);

	private bool l8l;

	private x.a<IAudioManager, AudioSource> l8L = new x.a<IAudioManager, AudioSource>();

	private x.a<IObjectManager, SceneEntity> l8w = new x.a<IObjectManager, SceneEntity>();

	private x.a<ILightManager, BaseLight> l8J = new x.a<ILightManager, BaseLight>();

	[CompilerGenerated]
	private bool l8B;

	[CompilerGenerated]
	private bool l8s;

	public List<ILightGroup> LightGroups => _LightGroups;

	public List<ISceneEntityGroup> EntityGroups => _EntityGroups;

	public string Name
	{
		get
		{
			return l8a;
		}
		set
		{
		}
	}

	public bool AssetInUse
	{
		[CompilerGenerated]
		get
		{
			return l8B;
		}
		[CompilerGenerated]
		internal set
		{
			l8B = value;
		}
	}

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8s;
		}
		[CompilerGenerated]
		set
		{
			l8s = value;
		}
	}

	internal string FileName
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	string B.J.ProjectFile => l8N;

	internal void _u0013_X(string param_0)
	{
		l8a = param_0;
	}

	public BaseScene()
		: this(param_0: true)
	{
	}

	protected BaseScene(bool param_0)
	{
		if (param_0)
		{
			SunBurnEditor.OnCreateResource(this);
		}
	}

	public void Dispose()
	{
		Clear();
		if (!l8l)
		{
			l8l = true;
			SunBurnEditor.OnDisposeResource(this);
		}
	}

	public void SetContainingManager<T>(IManager manager)
	{
		if ((object)typeof(T) == typeof(IAudioManager))
		{
			l8L.SetManager(manager as IAudioManager);
		}
		else if ((object)typeof(T) == typeof(IObjectManager))
		{
			l8w.SetManager(manager as IObjectManager);
		}
		else if ((object)typeof(T) == typeof(ILightManager))
		{
			l8J.SetManager(manager as ILightManager);
		}
		AssetInUse = l8L.ContainingManager != null || l8w.ContainingManager != null || l8J.ContainingManager != null;
		SunBurnEditor.NZ(this, s.N.InUse);
	}

	public void Clear()
	{
		_LightGroups.Clear();
		_EntityGroups.Clear();
		l8L.RemoveSubmittedObjects();
		l8w.RemoveSubmittedObjects();
		l8J.RemoveSubmittedObjects();
	}

	public void Apply()
	{
		l8L.RemoveSubmittedObjects();
		l8w.RemoveSubmittedObjects();
		l8J.RemoveSubmittedObjects();
		foreach (ISceneEntityGroup entityGroup in _EntityGroups)
		{
			for (int i = 0; i < entityGroup.Entities.Count; i++)
			{
				ISceneEntity sceneEntity = entityGroup.Entities[i];
				if (sceneEntity is SceneEntity)
				{
					l8w.Submit(sceneEntity as SceneEntity);
				}
				else if (sceneEntity is AudioSource)
				{
					l8L.Submit(sceneEntity as AudioSource);
				}
			}
		}
		foreach (ILightGroup lightGroup3 in _LightGroups)
		{
			if (lightGroup3.ShadowGroup && lightGroup3.ShadowRenderLightsTogether)
			{
				int num = 0;
				ILightGroup lightGroup = lightGroup3;
				for (int j = 0; j < lightGroup3.Lights.Count; j++)
				{
					if (num >= SunBurnCoreSystem.MaxLightsPerGroup)
					{
						LightGroup lightGroup2 = new LightGroup();
						lightGroup2.ld(lightGroup);
						lightGroup = lightGroup2;
						num = 0;
					}
					ILight light = lightGroup3.Lights[j];
					if (light is BaseLight obj)
					{
						light.ShadowSource = lightGroup;
						l8J.Submit(obj);
						num++;
					}
				}
				continue;
			}
			for (int k = 0; k < lightGroup3.Lights.Count; k++)
			{
				if (lightGroup3.Lights[k] is BaseLight obj2)
				{
					l8J.Submit(obj2);
				}
			}
		}
		l8L.Optimize();
		l8w.Optimize();
		l8J.Optimize();
	}

	public abstract void SetObjectData(SerializationInfo info, StreamingContext context);

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
}
