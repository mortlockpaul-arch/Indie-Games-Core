using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Manages all post processors and applies post processing effects to the scene.
/// </summary>
public class PostProcessManager : IPostProcessManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	private int fNB = 20;

	private IGraphicsDeviceService fNa;

	private bool fNg = true;

	private bool fNf = true;

	private List<IPostProcessor> fN_0003 = new List<IPostProcessor>();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.PostProcessManagerType;

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the IManagerServiceProvider. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest order value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	/// </summary>
	public int ManagerProcessOrder
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNa;

	/// <summary>
	/// Creates a PostProcessManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public PostProcessManager(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNa = graphicsdevicemanager;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fNg = preferences.PostProcessingDetail != DetailPreference.Off;
		for (int i = 0; i < fN_0003.Count; i++)
		{
			fN_0003[i].ApplyPreferences(preferences);
		}
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		if (!fNg)
		{
			return;
		}
		LightingSystemManager.Instance.aZ(fNa.GraphicsDevice, scenestate.FrameBuffers);
		_00037(scenestate);
		int num = 0;
		for (int i = 0; i < fN_0003.Count; i++)
		{
			if (fN_0003[i].BeginFrameRendering(scenestate, num))
			{
				num++;
			}
		}
	}

	/// <summary>
	/// Finalizes rendering and applies all post processing.
	/// </summary>
	public virtual void EndFrameRendering()
	{
		if (fNg)
		{
			for (int num = fN_0003.Count - 1; num >= 0; num--)
			{
				fN_0003[num].EndFrameRendering();
			}
		}
	}

	private void _00037(ISceneState P_0)
	{
		if (fNf)
		{
			fNf = false;
			for (int i = 0; i < fN_0003.Count; i++)
			{
				fN_0003[i].Unload();
			}
		}
	}

	private void _0003p(List<SurfaceFormat> P_0, SurfaceFormat[] P_1)
	{
		for (int i = 0; i < P_0.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < P_1.Length; j++)
			{
				if (P_0[i] == P_1[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				P_0.RemoveAt(i);
				i--;
			}
		}
	}

	/// <summary>
	/// Adds a new post processor to the processing chain. The last processor added
	/// to the chain is the first to apply its visual effects.
	/// </summary>
	/// <param name="postprocessor"></param>
	public void AddPostProcessor(IPostProcessor postprocessor)
	{
		fN_0003.Add(postprocessor);
		fNf = true;
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public virtual void Clear()
	{
	}

	/// <summary>
	/// Disposes any graphics resources used internally by this object, and removes
	/// resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public virtual void Unload()
	{
		for (int i = 0; i < fN_0003.Count; i++)
		{
			fN_0003[i].Unload();
		}
		fN_0003.Clear();
		fNf = true;
	}
}
