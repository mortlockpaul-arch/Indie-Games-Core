using System;
using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

public abstract class BaseLightMapManager : ILightMapManager, IManagerService, IManager, IUnloadable
{
	private int l8a = 30;

	private IManagerServiceProvider l8_u0013_;

	private Dictionary<int, LightMap> l8N = new Dictionary<int, LightMap>();

	private Dictionary<int, LightOcclusionBuffer> l8l = new Dictionary<int, LightOcclusionBuffer>();

	public Type ManagerType => typeof(ILightMapManager);

	public int ManagerProcessOrder
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public IManagerServiceProvider OwnerSceneInterface => l8_u0013_;

	internal Dictionary<int, LightMap> MeshLightMaps => l8N;

	internal Dictionary<int, LightOcclusionBuffer> LightOcclusionBuffers => l8l;

	public BaseLightMapManager(IManagerServiceProvider sceneinterface)
	{
		l8_u0013_ = sceneinterface;
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public virtual void Clear()
	{
	}

	public virtual void Unload()
	{
		foreach (KeyValuePair<int, LightMap> item in l8N)
		{
			item.Value?.Dispose();
		}
		l8N.Clear();
		l8l.Clear();
	}

	public abstract void SetLightMap(int meshuniqueid, LightMap lightmap);

	public abstract LightMap GetLightMap(RenderableMesh mesh);

	public abstract LightOcclusionBuffer GetLightOcclusionBuffer(ILight light);
}
