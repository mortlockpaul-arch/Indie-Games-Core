using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Base manager used for generating light maps.
/// </summary>
public abstract class BaseLightMapManager : ILightMapManager, IManagerService, IManager, IUnloadable
{
	private int fNB = 30;

	private IGraphicsDeviceService fNa;

	private IManagerServiceProvider fNg;

	private Dictionary<int, Texture2D> fNf = new Dictionary<int, Texture2D>();

	private Dictionary<int, LightOcclusionBuffer> fN_0003 = new Dictionary<int, LightOcclusionBuffer>();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => typeof(ILightMapManager);

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

	internal Dictionary<int, Texture2D> MeshLightMaps => fNf;

	internal Dictionary<int, LightOcclusionBuffer> LightOcclusionBuffers => fN_0003;

	/// <summary>
	/// Creates a new BaseLightMapManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public BaseLightMapManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNa = graphicsdevicemanager;
		fNg = sceneinterface;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Removes all objects from the container. Commonly used while clearing the scene.
	/// </summary>
	public virtual void Clear()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public virtual void Unload()
	{
		foreach (KeyValuePair<int, Texture2D> item in fNf)
		{
			item.Value?.Dispose();
		}
		fNf.Clear();
		fN_0003.Clear();
	}

	/// <summary>
	/// Gets the light map associated with the provided mesh or a default light map if non exists.
	/// </summary>
	/// <param name="mesh"></param>
	/// <returns></returns>
	public abstract Texture2D GetLightMap(RenderableMesh mesh);

	/// <summary>
	/// Gets the light occlusion buffer associated with the provided light or null if non exists.
	/// </summary>
	/// <param name="light"></param>
	/// <returns></returns>
	public abstract LightOcclusionBuffer GetLightOcclusionBuffer(ILight light);
}
