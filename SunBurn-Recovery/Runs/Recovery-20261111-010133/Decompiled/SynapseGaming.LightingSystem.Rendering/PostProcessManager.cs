using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public class PostProcessManager : IPostProcessManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	private int l8a = 20;

	private IGraphicsDeviceService l8_u0013_;

	private IManagerServiceProvider l8N;

	private bool l8l = true;

	private bool l8L = true;

	private List<IPostProcessor> l8w = new List<IPostProcessor>();

	public Type ManagerType => SceneInterface.PostProcessManagerType;

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

	public IGraphicsDeviceService GraphicsDeviceManager => l8_u0013_;

	public IManagerServiceProvider OwnerSceneInterface => l8N;

	public PostProcessManager(IManagerServiceProvider sceneinterface)
	{
		l8_u0013_ = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
		l8N = sceneinterface;
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
		l8l = preferences.PostProcessingDetail != DetailPreference.Off;
		for (int i = 0; i < l8w.Count; i++)
		{
			l8w[i].ApplyPreferences(preferences);
		}
	}

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		if (!l8l)
		{
			return;
		}
		SunBurnCoreSystem.Instance._u0013_M(scenestate.FrameBuffers);
		L_u0019_(scenestate);
		int num = 0;
		for (int i = 0; i < l8w.Count; i++)
		{
			if (l8w[i].BeginFrameRendering(scenestate, num))
			{
				num++;
			}
		}
	}

	public virtual void EndFrameRendering()
	{
		if (l8l)
		{
			for (int num = l8w.Count - 1; num >= 0; num--)
			{
				l8w[num].EndFrameRendering();
			}
		}
	}

	private void L_u0019_(ISceneState param_0)
	{
		if (l8L)
		{
			l8L = false;
			for (int i = 0; i < l8w.Count; i++)
			{
				l8w[i].Unload();
			}
		}
	}

	private void Ly(List<SurfaceFormat> param_0, SurfaceFormat[] param_1)
	{
		for (int i = 0; i < param_0.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < param_1.Length; j++)
			{
				if (param_0[i] == param_1[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				param_0.RemoveAt(i);
				i--;
			}
		}
	}

	public void AddPostProcessor(IPostProcessor postprocessor)
	{
		l8w.Add(postprocessor);
		l8L = true;
	}

	public void RemovePostProcessor(IPostProcessor postprocessor)
	{
		l8w.Remove(postprocessor);
		l8L = true;
	}

	public virtual void Clear()
	{
	}

	public virtual void Unload()
	{
		for (int i = 0; i < l8w.Count; i++)
		{
			l8w[i].Unload();
		}
		l8w.Clear();
		l8L = true;
	}
}
